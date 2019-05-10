using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Layers
{
    /// <summary>
    /// Layer to add perlin noise to the height map
    /// </summary>
    public class PerlinNoise : ALayer<HeightMapBuilder>
    {
        private readonly float _amplitude;
        private readonly Vector2 _offset;
        private readonly float _octavesStrength;
        private readonly int _octaves;

        /// <summary>
        /// Perlin noise layer
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="offset"></param>
        /// <param name="octaves">The amount of octaves in the perlin noise layer. Minimal input is 1</param>
        /// <param name="octavesStrength"></param>
        /// <param name="strength"></param>
        public PerlinNoise(float amplitude, Vector2 offset, int octaves = 1, float octavesStrength = 0.5f)
        {
            _amplitude = amplitude;
            _offset = offset;
            _octavesStrength = octavesStrength;
            _octaves = Mathf.Max(octaves, 1);
        }

        public override string Kernel()
        {
            return Constants.PerlinNoiseKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.PerlinAmplitudeAttribute, _amplitude);
            Shader.SetVector(Constants.PerlinOffsetAttribute, _offset);
            Shader.SetInt(Constants.PerlinOctavesAttribute, _octaves);
            Shader.SetFloat(Constants.PerlinOctavesStrengthAttribute, _octavesStrength);
        }
    }
}
