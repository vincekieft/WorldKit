// Variables
uint PseudoRandomNoiseSeed;

// Kernel
[numthreads(8,1,1)]
void PseudoRandomNoise (uint3 id : SV_DispatchThreadID)
{
    // Calculate vector
    uint2 pos = IndexToVector(id.x, HeightMapResolution);
    
    // Generate noise
    float noise = Random((pos.x + (pos.y * HeightMapResolution)) * PseudoRandomNoiseSeed);
    
    // Update buffer
    HeightBuffer[id.x] = lerp(HeightBuffer[id.x], noise, BlendStrength);
}
