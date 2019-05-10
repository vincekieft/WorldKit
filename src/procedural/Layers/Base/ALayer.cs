using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public abstract class ALayer<T> where T : ABuilder
    {
        private T _builder;

        /// <summary>
        /// Initialize a layer. Call this method before calling others
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Initialize(T builder)
        {
            _builder = builder;
        }

        public virtual Vector3 ThreadGroup(){
            return Builder.DefaultThreadGroups();
        }

        public virtual void Release(){}
        
        /// <summary>
        /// Name of the kernel of this layer
        /// </summary>
        /// <returns></returns>
        public abstract string Kernel();
        public abstract void SetAttributes(int kernel);

        protected T Builder => _builder;
        protected ComputeShader Shader => _builder.Shader;
    }
}
