// Licensed under the Non-Profit Open Software License version 3.0

// Variables
uint TerraceCount;
float TerraceShape;
bool Smooth;

// Kernel
[numthreads(1024,1,1)]
void Terrace (uint3 id : SV_DispatchThreadID)
{
    if(id.x < HeightMapLength){ // Keep in bounds
        float height = HeightBuffer[id.x] * TerraceCount; // Calculate height inside the terrace range
        uint heightFloor = uint(height); // Floor the height
        float difference = height - heightFloor; // Difference between height and floor. Normalized terrace
        
        if(Smooth){ // Should the terraces be smooth or sharp. Changes what part of the sigmoid is used
            difference = (NormalisedSigmoid(difference * 2 - 1, TerraceShape) + 1) / 2;
        } else {
            difference = NormalisedSigmoid(difference, TerraceShape);
        }
        
        // Calculate floor and ceil of terrace
        float floor = float(heightFloor) / TerraceCount;
        float ceil = float(heightFloor + 1) / TerraceCount;
        
        // Update buffer
        HeightBuffer[id.x] = lerp(floor, ceil, difference);
    }
}
