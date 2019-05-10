using UnityEngine;
using WorldKit.Procedural.Utils;
using WorldKit.src.procedural.Builders;
using WorldKit.src.procedural.Layers.Base;
using WorldKit.src.procedural.Utils;

namespace WorldKit.src.procedural.Layers
{
    /// <summary>
    /// Turns a height map into a texture
    /// </summary>
    public class HeightMapToTexture : ALayer<TextureBuilder>
    {
        private readonly int _resolution;
        private readonly ComputeBuffer _heightBuffer;
        
        /// <summary>
        /// Constructs a new height map to texture layer based on a height map float array
        /// </summary>
        /// <param name="heightMap">The height map to use</param>
        public HeightMapToTexture(float[] heightMap) : this(BufferUtils.CreateHeightBuffer(heightMap)) {}
        
        /// <summary>
        /// Constructs a new height map to texture layer based on a given height map buffer
        /// </summary>
        /// <param name="heightBuffer">The height map buffer to use</param>
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
