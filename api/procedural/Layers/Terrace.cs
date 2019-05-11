// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Layers
{
    /// <summary>
    /// Layer to add terrace effect to height map
    /// </summary>
    public class Terrace : ALayer<HeightMapBuilder>
    {
        private readonly int _terraceCount;
        private readonly float _shape;
        private readonly bool _smooth;

        /// <summary>
        /// Terrace layer
        /// </summary>
        /// <param name="terraceCount">How many terraces should there be</param>
        /// <param name="shape">How much of the original shape should come through. Clamped between -1 and 1, where 0 doesnt change the origional shape</param>
        /// <param name="smooth">Should the terraces be sharp or smooth</param>
        public Terrace(int terraceCount, float shape, bool smooth = false)
        {
            _terraceCount = terraceCount;
            _shape = Mathf.Clamp(shape, -1f, 1f);
            _smooth = smooth;
        }
        
        public override string Kernel()
        {
            return Constants.TerraceKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetInt(Constants.TerraceCountAttribute, _terraceCount);
            Shader.SetFloat(Constants.TerraceShapeAttribute, _shape);
            Shader.SetBool(Constants.TerraceSmoothAttribute, _smooth);
        }
    }
}