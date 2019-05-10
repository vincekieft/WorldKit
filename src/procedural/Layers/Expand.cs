using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class Expand : ALayer<ABuilder>
    {
        private readonly Vector2 _minMax;
        private readonly float _strength;

        public Expand(Vector2 minMax, float strength = 1f)
        {
            _minMax = minMax;
            _strength = strength;
        }
        
        public override string Kernel()
        {
            return Constants.ExpandKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetVector(Constants.ExpandMinMaxAttribute, _minMax);
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
        }
    }
}