using UnityEngine;
using WorldKit.Procedural.Layers;
using WorldKit.Procedural.Utils;

namespace WorldKit.Procedural.Builders
{
    public class HeightMapBuilder : ABuilder
    {
        private readonly ComputeBuffer _heightBuffer;
        private readonly int _bufferLength;
        private readonly int _resolution;
        
        public HeightMapBuilder(ComputeShader shader, int resolution) : this(shader, BufferUtils.CreateHeightBuffer(resolution)){}
        public HeightMapBuilder(ComputeShader shader, float[] heightMap) : this(shader, BufferUtils.CreateHeightBuffer(heightMap)) {}
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

        public float[] HeightMap()
        {
            float[] heights = new float[_bufferLength];
            _heightBuffer.GetData(heights);
            return heights;
        }
        
        public ComputeBuffer HeightBuffer()
        {
            return _heightBuffer;
        }

        public int Resolution => _resolution;
    }
}