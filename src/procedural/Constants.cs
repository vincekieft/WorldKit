namespace WorldKit.Procedural
{
    public static class Constants
    {
        // Global
        public const int GpuGridSize = 8;
        public const int MaxMeshVertexCount = 65000;

        // Kernels
        public const string ClampKernel = "Clamp";
        public const string HeightMapToMeshKernel = "HeightMapToMesh";
        public const string HeightMapToTextureKernel = "HeightMapToTexture";
        public const string ErosionKernel = "Erosion";
        public const string PerlinNoiseKernel = "PerlinNoise";
        public const string PseudoRandomNoiseKernel = "PseudoRandomNoise";
        public const string HeightMapToVerticesKernel = "HeightMapToVertices";
        public const string ExpandKernel = "Expand";
        public const string HeightContrastKernel = "HeightContrast";
        public const string TerraceKernel = "Terrace";
        
        // Buffers
        public const string HeightBuffer = "HeightBuffer";
        public const string VertexBuffer = "VertexBuffer";
        public const string TextureBuffer = "Texture";
        
        // Attributes
        public const string HeightMapResolutionAttribute = "HeightMapResolution";
        public const string HeightMapLengthAttribute = "HeightMapLength";
        public const string VertexCountAttribute = "VertexCount";
        public const string BlendStrengthAttribute = "BlendStrength";
        public const string ClampRangeAttribute = "ClampRange";
        public const string TerrainExtendAttribute = "TerrainExtend";
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
        public const string PerlinAmplitudeAttribute = "PerlinAmplitude";
        public const string PerlinOffsetAttribute = "PerlinOffset";
        public const string PerlinOctavesAttribute = "PerlinOctaves";
        public const string PerlinOctavesStrengthAttribute = "PerlinOctavesStrength";
        public const string PseudoRandomNoiseSeedAttribute = "PseudoRandomNoiseSeed";
        public const string MeshExtendsAttribute = "MeshExtends";
        public const string VertexResolutionAttribute = "VertexResolution";
        public const string ErosionBrushRadiusAttribute = "ErosionBrushRadius";
        public const string ExpandMinMaxAttribute = "ExpanderMinMax";
        public const string HeightContrastAttribute = "HeightContrastPow";
        public const string TerraceCountAttribute = "TerraceCount";
        public const string TerraceSmoothAttribute = "Smooth";
        public const string TerraceShapeAttribute = "TerraceShape";
    }
}