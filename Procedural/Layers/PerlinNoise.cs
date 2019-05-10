using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class PerlinNoise : ALayer<ABuilder>
    {
        private readonly float _amplitude;
        private readonly Vector2 _offset;
        private readonly float _strength;
        
        public PerlinNoise(float amplitude, Vector2 offset, float strength = 1f)
        {
            _amplitude = amplitude;
            _offset = offset;
            _strength = strength;
        }

        public override string Kernel()
        {
            return Constants.PerlinNoiseKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
            Shader.SetFloat(Constants.PerlinAmplitudeAttribute, _amplitude);
            Shader.SetVector(Constants.PerlinOffsetAttribute, _offset);
        }
    }
}
