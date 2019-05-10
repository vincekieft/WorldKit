// Variables
float2 ClampRange;

// Kernel
[numthreads(8,8,1)]
void Clamp (uint3 id : SV_DispatchThreadID)
{
    // Read data
    float height = Texture[id.xy].r;

    // Update texture
    Texture[id.xy] = lerp(height, clamp(height, ClampRange.x, ClampRange.y), BlendStrength);
}

