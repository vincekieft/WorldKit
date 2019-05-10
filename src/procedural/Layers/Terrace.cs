using System;
using UnityEngine;
using WorldKit.Procedural;
using WorldKit.Procedural.Builders;
using WorldKit.Procedural.Layers;

namespace WorldKit.src.procedural.Layers
{
    public class Terrace : ALayer<ABuilder>
    {
        private readonly int _terraceCount;
        private readonly float _shape;
        private readonly bool _smooth;
        private readonly float _strength;

        /// <summary>
        /// Terrace layer
        /// </summary>
        /// <param name="terraceCount">How many terraces should there be</param>
        /// <param name="shape">How much of the original shape should come through. Clamped between -1 and 1, where 0 doesnt change the origional shape</param>
        /// <param name="smooth">Should the terraces be sharp or smooth</param>
        /// <param name="strength"></param>
        public Terrace(int terraceCount, float shape, bool smooth = false, float strength = 1f)
        {
            _terraceCount = terraceCount;
            _shape = Mathf.Clamp(shape, -1f, 1f);
            _smooth = smooth;
            _strength = strength;
        }
        
        public override string Kernel()
        {
            return Constants.TerraceKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetInt(Constants.TerraceCountAttribute, _terraceCount);
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
            Shader.SetFloat(Constants.TerraceShapeAttribute, _shape);
            Shader.SetBool(Constants.TerraceSmoothAttribute, _smooth);
        }
    }
}