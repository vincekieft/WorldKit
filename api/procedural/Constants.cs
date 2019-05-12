// Licensed under the Non-Profit Open Software License version 3.0

namespace WorldKit.api.procedural
{
    public static class Constants
    {
        // Global
        public const int GpuGridSize = 32;
        public const int MaxMeshVertexCount = 65000;
        
        // Erosion constants
        public const float Gravity = 9.81f;

        // Kernels
        public const string ClampKernel = "Clamp";
        public const string HeightMapToTextureKernel = "HeightMapToTexture";
        public const string HydraulicErosionKernel = "HydraulicErosion";
        public const string PerlinNoiseKernel = "PerlinNoise";
        public const string PseudoRandomNoiseKernel = "PseudoRandomNoise";
        public const string ExpandKernel = "Expand";
        public const string HeightContrastKernel = "HeightContrast";
        public const string TerraceKernel = "Terrace";
        
        // Buffers
        public const string HeightBuffer = "HeightBuffer";
        public const string TextureBuffer = "Texture";
        
        // Global attributes
        public const string HeightMapResolutionAttribute = "HeightMapResolution";
        public const string HeightMapLengthAttribute = "HeightMapLength";
        public const string BlendStrengthAttribute = "BlendStrength";
        
        // Clamp attributes
        public const string ClampRangeAttribute = "ClampRange";
        
        // Erosion attributes
        public const string ErosionMaxLifeTimeAttribute = "ErosionMaxLifeTime";
        public const string ErosionInertiaAttribute = "ErosionInertia";
        public const string ErosionParticleStartSpeedAttribute = "ErosionParticleStartSpeed";
        public const string ErosionParticleStartWaterAttribute = "ErosionParticleStartWater";
        public const string ErosionSedimentCapacityFactorAttribute = "ErosionSedimentCapacityFactor";
        public const string ErosionMinSedimentCapacityAttribute = "ErosionMinSedimentCapacity";
        public const string ErosionSpeedAttribute = "ErosionSpeed";
        public const string ErosionEvaporateSpeedAttribute = "ErosionEvaporateSpeed";
        public const string ErosionGravityAttribute = "ErosionGravity";
        public const string ErosionDepositSpeedAttribute = "ErosionDepositSpeed";
        public const string ErosionParticleCount = "ErosionParticleCount";
        
        // Perlin noise attributes
        public const string PerlinTypeAttribute = "PerlinType";
        public const string PerlinFrequencyAttribute = "PerlinFrequency";
        public const string PerlinOffsetAttribute = "PerlinOffset";
        public const string PerlinOctavesAttribute = "PerlinOctaves";
        public const string PerlinOctavesStrengthAttribute = "PerlinOctavesStrength";
        
        // Pseudo random noise attributes
        public const string PseudoRandomNoiseSeedAttribute = "PseudoRandomNoiseSeed";

        // Expand attributes
        public const string ExpandMinMaxAttribute = "ExpanderMinMax";
        public const string HeightContrastAttribute = "HeightContrastPow";
        
        // Terrace attributes
        public const string TerraceCountAttribute = "TerraceCount";
        public const string TerraceSmoothAttribute = "Smooth";
        public const string TerraceShapeAttribute = "TerraceShape";
    }
}
