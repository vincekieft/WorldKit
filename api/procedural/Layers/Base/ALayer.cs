// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Builders;

namespace WorldKit.api.procedural.Layers.Base
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
