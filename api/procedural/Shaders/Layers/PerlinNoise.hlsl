// Licensed under the Non-Profit Open Software License version 3.0

// Variables
uint PerlinType;
float PerlinAmplitude;
float PerlinFrequency;
float2 PerlinOffset;
uint PerlinOctaves;
float PerlinOctavesStrength;

// Methods
float StandardPerlinNoise(uint id, float amplitude, float2 offset){
    // Calculate vector
    float2 pos = IndexToFloatVector(id, HeightMapResolution);
    
    // Calculate x and y position
    float x = float(pos.x) / HeightMapResolution;
    float y = float(pos.y) / HeightMapResolution;
    
    // Generate noise
    return cnoise(float2(x * amplitude + offset.x, y * amplitude + offset.y));
}

float BillowyPerlinNoise(uint id, float amplitude, float2 offset){
    float noise = StandardPerlinNoise(id, amplitude, offset);
    return abs(noise * 2 - 1);
}

float RigidPerlinNoise(uint id, float amplitude, float2 offset){
    return 1 - BillowyPerlinNoise(id, amplitude, offset);;
}

// Kernel
[numthreads(32,1,1)]
void PerlinNoise (uint3 id : SV_DispatchThreadID)
{
    if(id.x < HeightMapLength){ // Keep in bounds
        float total = 0;
        float frequency = PerlinFrequency;
        float amplitude = 1;
        float maxvalue = 0;
        
        for(uint i = 0; i < PerlinOctaves; i++){
        
            // Types
            if(PerlinType == 0){ // Standard
                total += StandardPerlinNoise(id.x, frequency, PerlinOffset) * amplitude;
            } else if(PerlinType == 1){ // Billowy
                total += BillowyPerlinNoise(id.x, frequency, PerlinOffset) * amplitude;
            } else if(PerlinType == 2){ // Rigid
                total += RigidPerlinNoise(id.x, frequency, PerlinOffset) * amplitude;
            }
            
            maxvalue += amplitude;
            amplitude *= PerlinOctavesStrength;
            frequency *= 2;
        }
        
        // Update buffer
        HeightBuffer[id.x] = total/maxvalue;
    }
}
