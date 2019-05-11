// Licensed under the Non-Profit Open Software License version 3.0

float Magnitude2D(float2 dir){
    return max(0.01,sqrt(dir.x * dir.x + dir.y * dir.y));
}

float Magnitude3D(float3 dir){
    return max(0.01,sqrt(dir.x * dir.x + dir.y * dir.y + dir.z * dir.z));
}

float2 Normalize2D(float2 dir){
    float magnitude = Magnitude2D(dir);
    dir.x /= magnitude;
    dir.y /= magnitude;
    return dir;
}

float3 Normalize3D(float3 dir){
    float magnitude = Magnitude3D(dir);
    dir.x /= magnitude;
    dir.y /= magnitude;
    dir.z /= magnitude;
    return dir;
}

int PosToIndex(uint2 pos, int width){
    return pos.x + (pos.y * width);
}

uint2 IndexToVector(int index, int width){
    float division = float(index) / float(width);
    int rounded = int(division);
    return uint2((division - float(rounded)) * width,rounded);
}

float2 IndexToFloatVector(int index, int width){
    float division = float(index) / float(width);
    int rounded = int(division);
    return float2((division - float(rounded)) * width,rounded);
}


float2 RelativizeVector(float x, float y, float w, float h){
     return float2(
         x / w,
         y / h
     );
}

float3 RelativizeVector(float3 vec, float3 size){
     return float3(
         vec.x / size.x,
         vec.y / size.y,
         vec.z / size.z
     );
 }
 
 void AddToBuffer(int index, int resolution, float2 cellOffset, float value, RWStructuredBuffer<float> buffer){
    buffer[index] += value * (1 - cellOffset.x) * (1 - cellOffset.y);
    buffer[index + 1] += value * cellOffset.x * (1 - cellOffset.y);
    buffer[index + resolution] += value * (1 - cellOffset.x) * cellOffset.y;
    buffer[index + resolution + 1] += value * cellOffset.x * cellOffset.y;
}
 
 float3 CalculateNormal(uint2 pos, int resolution, float3 size, RWStructuredBuffer<float> buffer){
    // Calculate multiplier direction
    float multiplier = -1;
    if(pos.x <= 0 || pos.y <= 0){ multiplier = 1; }
    
    // Calculate left, right, up and down vertex positions
    float3 c = float3(pos.x, 0, pos.y);
    float3 l = float3(pos.x - 1, 0, pos.y);
    float3 u = float3(pos.x, 0, pos.y - 1);
 
    // Gather heights for left, right, up and down vertices
    c.y = buffer[PosToIndex(uint2(c.xz), resolution)] * size.y;
    l.y = buffer[PosToIndex(uint2(l.xz), resolution)] * size.y;
    u.y = buffer[PosToIndex(uint2(u.xz), resolution)] * size.y;
    
    // calculate normal direction
    return Normalize3D(cross(l - c, u - c)) * multiplier;
 }
 
 float NormalisedSigmoid(float x, float k){
    return (x - x * k) / (k - abs(x) * 2 * k + 1);
 }

float3 CalculateGradientAndHeight (float2 pos, int resolution, RWStructuredBuffer<float> buffer) {
    // Calculate coords
    uint2 coord = uint2(
        (int) pos.x,
        (int) pos.y
    );

    // Calculate index
    int index = PosToIndex(coord, resolution);

    // Calculate droplet's offset inside the cell (0,0) = at NW node, (1,1) = at SE node
    float x = pos.x - float(coord.x);
    float y = pos.y - float(coord.y);

    // Calculate heights of the four nodes of the droplet's cell
    float heightNW = buffer[index];
    float heightNE = buffer[index + 1];
    float heightSW = buffer[index + HeightMapResolution];
    float heightSE = buffer[index + HeightMapResolution + 1];
    
    // Calculate droplet's direction of flow with bilinear interpolation of height difference along the edges
    float gradientX = (heightNE - heightNW) * (1 - y) + (heightSE - heightSW) * y;
    float gradientY = (heightSW - heightNW) * (1 - x) + (heightSE - heightNE) * x;

    // Calculate height with bilinear interpolation of the heights of the nodes of the cell
    float height = heightNW * (1 - x) * (1 - y) + heightNE * x * (1 - y) + heightSW * (1 - x) * y + heightSE * x * y;
    
    return float3(gradientX,gradientY,height);
}