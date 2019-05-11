// Licensed under the Non-Profit Open Software License version 3.0

// Variables
uint PseudoRandomNoiseSeed;

// Kernel
[numthreads(32,1,1)]
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
