#include "ClassicNoise2D.hlsl"

// Variables
float PerlinAmplitude;
float2 PerlinOffset;

// Kernel
[numthreads(8,1,1)]
void PerlinNoise (uint3 id : SV_DispatchThreadID)
{
    if(id.x < HeightMapLength){ // Keep in bounds
        // Calculate vector
        float2 pos = IndexToFloatVector(id.x, HeightMapResolution);
        
        // Calculate x and y position
        float x = float(pos.x) / HeightMapResolution;
        float y = float(pos.y) / HeightMapResolution;
        
        // Generate noise
        float noise = cnoise(float2(x * PerlinAmplitude + PerlinOffset.x, y * PerlinAmplitude + PerlinOffset.y));
        
        // Update buffer
        HeightBuffer[id.x] = lerp(HeightBuffer[id.x], noise, BlendStrength);
    }
}
