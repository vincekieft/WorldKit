// Licensed under the Non-Profit Open Software License version 3.0

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
        private readonly PerlinType _type;
        private readonly float _frequency;
        private readonly Vector2 _offset;
        private readonly float _octavesStrength;
        private readonly int _octaves;

        /// <summary>
        /// Perlin noise layer
        /// </summary>
        /// <param name="type">The type of perlin noise</param>
        /// <param name="frequency">How frequent the noise should be. Higher means smaller shapes and more noise</param>
        /// <param name="offset">Offset the noise position</param>
        /// <param name="octaves">The amount of octaves in the perlin noise layer. Minimal input is 1</param>
        /// <param name="octavesStrength">How much of the octaves should shine through</param>
        public PerlinNoise(PerlinType type, float frequency, int octaves = 1, float octavesStrength = 0.5f, Vector2 offset = default)
        {
            _type = type;
            _frequency = frequency;
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
            Shader.SetInt(Constants.PerlinTypeAttribute, (int)_type);
            Shader.SetFloat(Constants.PerlinFrequencyAttribute, _frequency);
            Shader.SetVector(Constants.PerlinOffsetAttribute, _offset);
            Shader.SetInt(Constants.PerlinOctavesAttribute, _octaves);
            Shader.SetFloat(Constants.PerlinOctavesStrengthAttribute, _octavesStrength);
        }
        
        public enum PerlinType
        {
            Standard = 0,
            Billowy = 1,
            Rigid = 2
        }
    }
}
