// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Utils;

namespace WorldKit.api.procedural.Builders
{
    /// <summary>
    /// Builder user to to build height maps
    /// </summary>
    public class HeightMapBuilder : ABuilder
    {
        private readonly ComputeBuffer _heightBuffer;
        private readonly int _bufferLength;
        private readonly int _resolution;
        
        /// <summary>
        /// Constructs HeightMapBuilder and creates a new height map buffer based on the given resolution
        /// </summary>
        /// <param name="shader">The compute shader that has all the layer kernels</param>
        /// <param name="resolution">Resolution for the new height map</param>
        public HeightMapBuilder(ComputeShader shader, int resolution) : this(shader, BufferUtils.CreateHeightBuffer(resolution)){}
        
        /// <summary>
        /// Constructs HeightMapBuilder and takes a height map in the form of a float array as input
        /// </summary>
        /// <param name="shader">The compute shader that has all the layer kernels</param>
        /// <param name="heightMap">The height map to use. Height map length must have a whole square root result</param>
        public HeightMapBuilder(ComputeShader shader, float[] heightMap) : this(shader, BufferUtils.CreateHeightBuffer(heightMap)) {}
        
        /// <summary>
        /// Constructs HeightMapBuilder and takes a compute buffer containing a height map as input
        /// </summary>
        /// <param name="shader">The compute shader that has all the layer kernels</param>
        /// <param name="heightBuffer">Height map buffer used by this builder</param>
        public HeightMapBuilder(ComputeShader shader, ComputeBuffer heightBuffer) : base(shader)
        {
            _bufferLength = heightBuffer.count;
            _resolution = (int)Mathf.Sqrt(_bufferLength);
            _heightBuffer = heightBuffer;
        }

        protected override void InitializeKernel(int kernel)
        {
            Shader.SetBuffer(kernel, Constants.HeightBuffer, _heightBuffer);
            Shader.SetInt(Constants.HeightMapResolutionAttribute, _resolution);
            Shader.SetInt(Constants.HeightMapLengthAttribute, _bufferLength);
        }
        
        public override void Release()
        {
            _heightBuffer.Release();
        }

        public override Vector3 DefaultThreadGroups()
        {
            return new Vector3(Mathf.CeilToInt((float)_bufferLength / Constants.GpuGridSize), 1, 1);
        }

        /// <summary>
        /// Returns the resulting height map
        /// </summary>
        /// <returns></returns>
        public float[] HeightMap()
        {
            float[] heights = new float[_bufferLength];
            _heightBuffer.GetData(heights);
            return heights;
        }
        
        /// <summary>
        /// Returns the height map buffer used by this builder
        /// </summary>
        /// <returns></returns>
        public ComputeBuffer HeightBuffer()
        {
            return _heightBuffer;
        }

        /// <summary>
        /// The resulotion of the created height map
        /// </summary>
        public int Resolution => _resolution;
    }
}
