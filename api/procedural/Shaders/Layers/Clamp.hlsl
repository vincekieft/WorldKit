// Licensed under the Non-Profit Open Software License version 3.0

// Variables
float2 ClampRange;

// Kernel
[numthreads(32,1,1)]
void Clamp (uint3 id : SV_DispatchThreadID)
{
    if(id.x < HeightMapLength){ // Keep in bounds
        // Update texture
        HeightBuffer[id.x] = clamp(HeightBuffer[id.x], ClampRange.x, ClampRange.y);
    }
}

