using UnityEngine;
using WorldKit.Procedural.Builders;
using WorldKit.Procedural.Utils;

namespace WorldKit.Procedural.Layers
{
    public class HeightMapToTexture : ALayer<ABuilder>
    {
        private readonly int _resolution;
        private readonly ComputeBuffer _heightBuffer;
        
        public HeightMapToTexture(float[] heightMap) : this(BufferUtils.CreateHeightBuffer(heightMap)) {}
        public HeightMapToTexture(ComputeBuffer heightBuffer)
        {
            _resolution = (int)Mathf.Sqrt(heightBuffer.count);
            _heightBuffer = heightBuffer;
        }
        
        public override string Kernel()
        {
            return Constants.HeightMapToTextureKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetInt(Constants.HeightMapResolutionAttribute, _resolution);
            Shader.SetBuffer(kernel, Constants.HeightBuffer, _heightBuffer);
        }
    }
}
