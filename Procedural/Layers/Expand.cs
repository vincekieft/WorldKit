using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class Expand : ALayer<ABuilder>
    {
        private readonly Vector2 _minMax;

        public Expand(Vector2 minMax)
        {
            _minMax = minMax;
        }
        
        public override string Kernel()
        {
            return Constants.ExpandKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetVector(Constants.ExpandMinMaxAttribute, _minMax);
        }
    }
}