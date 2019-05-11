// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;
using WorldKit.api.procedural.Utils;

namespace WorldKit.api.procedural.Layers
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
