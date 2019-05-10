// Variables
float PerlinAmplitude;
float PerlinePersistence;
float2 PerlinOffset;
uint PerlinOctaves;
float PerlinOctavesStrength;

// Methods
float PerlinNoise(uint id, float amplitude, float2 offset){
    // Calculate vector
    float2 pos = IndexToFloatVector(id, HeightMapResolution);
    
    // Calculate x and y position
    float x = float(pos.x) / HeightMapResolution;
    float y = float(pos.y) / HeightMapResolution;
    
    // Generate noise
    return cnoise(float2(x * amplitude + offset.x, y * amplitude + offset.y));
}

// Kernel
[numthreads(32,1,1)]
void PerlinNoise (uint3 id : SV_DispatchThreadID)
{
    if(id.x < HeightMapLength){ // Keep in bounds
        float baseNoise = PerlinNoise(id.x, PerlinAmplitude, PerlinOffset);
        float noise = 0; // Perlin noise starts at zero
        float octaveStrength = 1; // Determines how much of each octave should be added to the final result
        
        // Loop through all the octaves
        for(uint i = 0; i < PerlinOctaves; i++){
            octaveStrength *= 0.5; // Times halve to lower the influence of the octave on the final result
            noise += PerlinNoise(id.x, pow(abs(PerlinAmplitude), (i + 1)), float2(PerlinOffset.x + (HeightMapResolution * i), PerlinOffset.y + (HeightMapResolution * i))) * octaveStrength;   
        }
        
        // Update buffer
        HeightBuffer[id.x] = lerp(baseNoise, noise, PerlinOctavesStrength);
    }
}
