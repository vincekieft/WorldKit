using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class HeightMapToVertices : ALayer<ABuilder>
    {
        private readonly ComputeBuffer _heightBuffer;

        public HeightMapToVertices(ComputeBuffer heightBuffer)
        {
            _heightBuffer = heightBuffer;
        }
        
        public override string Kernel()
        {
            return Constants.HeightMapToVerticesKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetBuffer(kernel, Constants.HeightBuffer, _heightBuffer);
        }
    }
}