using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class Clamp : ALayer<ABuilder>
    {
        private readonly Vector2 _range;
        private readonly float _strength;
        
        public Clamp(float min, float max, float strength = 1f)
        {
            _range = new Vector2(min, max);
            _strength = strength;
        }

        public override string Kernel()
        {
            return Constants.ClampKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
            Shader.SetVector(Constants.ClampRangeAttribute, _range);
        }
    }
}
