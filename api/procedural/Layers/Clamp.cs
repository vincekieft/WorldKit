// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Layers
{
    /// <summary>
    /// Layer to clamp the height map heights between a given range. Does cut of the heights that fall out of the range
    /// </summary>
    public class Clamp : ALayer<HeightMapBuilder>
    {
        private readonly Vector2 _range;
        
        /// <summary>
        /// Constructs a new clamp layer with a given min and max.
        /// </summary>
        /// <param name="min">Minimal height</param>
        /// <param name="max">Maximal height</param>
        public Clamp(float min, float max)
        {
            _range = new Vector2(min, max);
        }

        public override string Kernel()
        {
            return Constants.ClampKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetVector(Constants.ClampRangeAttribute, _range);
        }
    }
}
