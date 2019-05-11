// Licensed under the Non-Profit Open Software License version 3.0

// Variables
float HeightContrastPow;

// Kernel
[numthreads(32,1,1)]
void HeightContrast (uint3 id : SV_DispatchThreadID)
{
    // Expand height
    HeightBuffer[id.x] = clamp(pow(abs(HeightBuffer[id.x] + 0.5), HeightContrastPow) - 0.5, 0, 1);
}
