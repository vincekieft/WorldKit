// Licensed under the Non-Profit Open Software License version 3.0

using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Layers
{
    /// <summary>
    /// Layer to add more contrast to the heights
    /// </summary>
    public class HeightContrast : ALayer<ABuilder>
    {
        private readonly float _contrast;

        /// <summary>
        /// Add more contrast to a height map. Darker values get darker and bright values get brighter
        /// </summary>
        /// <param name="contrast">The amount of contrast to add. 1 means that the contrast stays the same. > 1 for more contrast></param>
        public HeightContrast(float contrast)
        {
            _contrast = contrast;
        }
        
        public override string Kernel()
        {
            return Constants.HeightContrastKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.HeightContrastAttribute, _contrast);
        }
    }
}