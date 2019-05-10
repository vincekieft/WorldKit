using UnityEngine;
using WorldKit.src.procedural.Builders;
using WorldKit.src.procedural.Layers.Base;

namespace WorldKit.src.procedural.Layers
{
    /// <summary>
    /// Layer to add pseudo random noise to height map
    /// </summary>
    public class PseudoRandomNoise : ALayer<HeightMapBuilder>
    {
        private readonly int _seed;

        /// <summary>
        /// Constructs a pseudo random noise layer
        /// </summary>
        /// <param name="seed">Seed for the noise</param>
        public PseudoRandomNoise(int seed)
        {
            _seed = Mathf.Clamp(seed, 1, 10000);
        }

        public override string Kernel()
        {
            return Constants.PseudoRandomNoiseKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetInt(Constants.PseudoRandomNoiseSeedAttribute, _seed);
        }
    }
}
