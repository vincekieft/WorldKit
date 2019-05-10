using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class HeightContrast : ALayer<ABuilder>
    {
        private readonly float _contrast;
        private readonly float _strength;

        /// <summary>
        /// Add more contrast to a heightmap. Darker values get darker and bright values get brighter
        /// </summary>
        /// <param name="contrast">The amount of contrast to add. 1 means that the contrast stays the same. > 1 for more contrast></param>
        /// <param name="strength"></param>
        public HeightContrast(float contrast, float strength = 1f)
        {
            _contrast = contrast;
            _strength = strength;
        }
        
        public override string Kernel()
        {
            return Constants.HeightContrastKernel;
        }

        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.HeightContrastAttribute, _contrast);
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
        }
    }
}