// Variables
uint PseudoRandomNoiseSeed;

// Kernel
[numthreads(8,1,1)]
void PseudoRandomNoise (uint3 id : SV_DispatchThreadID)
{
    if(id.x < HeightMapLength){ // Keep in bounds
        // Calculate vector
        uint2 pos = IndexToVector(id.x, HeightMapResolution);
        
        // Generate noise
        float noise = Random((pos.x + (pos.y * HeightMapResolution)) * PseudoRandomNoiseSeed);
        
        // Update buffer
        HeightBuffer[id.x] = noise;
    }
}
