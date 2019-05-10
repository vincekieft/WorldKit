// Variables
float2 ExpanderMinMax;

// Kernel
[numthreads(8,1,1)]
void Expand (uint3 id : SV_DispatchThreadID)
{
    // Expand height
    HeightBuffer[id.x] = clamp((HeightBuffer[id.x] - ExpanderMinMax.x) / ExpanderMinMax.y, 0, 1);
}
