using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class PseudoRandomNoise : ALayer<ABuilder>
    {
        private readonly int _seed;
        private readonly float _strength;
        
        public PseudoRandomNoise(int seed, float strength = 1f)
        {
            _seed = Mathf.Clamp(seed, 1, 10000);
            _strength = strength;
        }

        public override string Kernel()
        {
            return Constants.PseudoRandomNoiseKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
            Shader.SetInt(Constants.PseudoRandomNoiseSeedAttribute, _seed);
        }
    }
}
