// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Utils;

namespace WorldKit.api.procedural.Builders
{
    public class TextureBuilder : ABuilder
    {
        private readonly int _resolution;
        private readonly RenderTexture _texture;

        /// <summary>
        /// Instantiate new GpuTextureBuilder with given resolution. Creates a new RenderTexture
        /// </summary>
        /// <param name="shader">Compute shader to use. Pass this along to chained GpuTextureBuilders</param>
        /// <param name="resolution">Resolution of the new RenderTexture</param>
        public TextureBuilder(ComputeShader shader, int resolution) : this(shader,BufferUtils.CreateRenderTexture(resolution)){}

        /// <summary>
        /// Instantiate new GpuTextureBuilder based on a existing RenderTexture
        /// </summary>
        /// <param name="shader">Compute shader to use. Pass this along to chained GpuTextureBuilders</param>
        /// <param name="texture">RenderTexture with enableRandomWrite == true and already Created</param>
        public TextureBuilder(ComputeShader shader, RenderTexture texture) : base(shader)
        {
            this._resolution = texture.width;
            this._texture = texture;
        }
        
        protected override void InitializeKernel(int kernel)
        {
            Shader.SetTexture(kernel, Constants.TextureBuffer, _texture);
        }

        public override void Release()
        {
            _texture.Release();
        }

        public override Vector3 DefaultThreadGroups()
        {
            return new Vector3(TextureThreadGroupCount, TextureThreadGroupCount, 1);
        }

        private int TextureThreadGroupCount => _resolution / Constants.GpuGridSize;

        public RenderTexture Texture => _texture;
    }
}
