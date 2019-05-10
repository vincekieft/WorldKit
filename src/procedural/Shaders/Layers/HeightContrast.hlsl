// Variables
float HeightContrastPow;

// Kernel
[numthreads(8,1,1)]
void HeightContrast (uint3 id : SV_DispatchThreadID)
{
    // Expand height
    HeightBuffer[id.x] = clamp(pow(abs(HeightBuffer[id.x] + 0.5), HeightContrastPow) - 0.5, 0, 1);
}
