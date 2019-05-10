using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class PerlinNoise : ALayer<ABuilder>
    {
        private readonly float _amplitude;
        private readonly Vector2 _offset;
        private readonly float _octavesStrength;
        private readonly int _octaves;
        private readonly float _strength;

        /// <summary>
        /// Perlin noise layer
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="offset"></param>
        /// <param name="octaves">The amount of octaves in the perlin noise layer. Minimal input is 1</param>
        /// <param name="octavesStrength"></param>
        /// <param name="strength"></param>
        public PerlinNoise(float amplitude, Vector2 offset, int octaves = 1, float octavesStrength = 0.5f, float strength = 1f)
        {
            _amplitude = amplitude;
            _offset = offset;
            _octavesStrength = octavesStrength;
            _octaves = Mathf.Max(octaves, 1);
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
            Shader.SetInt(Constants.PerlinOctavesAttribute, _octaves);
            Shader.SetFloat(Constants.PerlinOctavesStrengthAttribute, _octavesStrength);
        }
    }
}
