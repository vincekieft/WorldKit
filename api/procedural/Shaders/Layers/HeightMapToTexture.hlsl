// Licensed under the Non-Profit Open Software License version 3.0

// Kernel
[numthreads(32,32,1)]
void HeightMapToTexture (uint3 id : SV_DispatchThreadID)
{
    // Get width and height of texture
    int w,h;
    Texture.GetDimensions(w,h);
    
    // Calculate position relative to heightmap
    float2 relativeID = RelativizeVector(id.x, id.y, w, h);
    float2 heightMapPos = float2(relativeID.x * HeightMapResolution, relativeID.y * HeightMapResolution);
    
    // Calculate height
    float3 gradient = CalculateGradientAndHeight(heightMapPos, HeightMapResolution, HeightBuffer); 
    
    // Set height
    
    Texture[id.xy] = float4(gradient.z,gradient.z,gradient.z,1);
}
