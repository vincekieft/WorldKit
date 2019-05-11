// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Layers
{
    /// <summary>
    /// Expands the heights to fit a new min max range.
    /// A min max of 0.2 to 0.5 takes all the heights in the height map and squeezes them into the new 0.2 and 0.5 range, keeping their relative height in mind.
    /// </summary>
    public class Expand : ALayer<HeightMapBuilder>
    {
        private readonly Vector2 _minMax;

        /// <summary>
        /// Constructs a new expand layer
        /// </summary>
        /// <param name="min">minimal range</param>
        /// <param name="max">maximal range</param>
        public Expand(float min, float max)
        {
            _minMax = new Vector2(min, max);
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