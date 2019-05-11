// Licensed under the Non-Profit Open Software License version 3.0

using System.Collections.Generic;
using UnityEngine;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Builders
{
    public abstract class ABuilder
    {
        private Dictionary<string, int> _kernelIds = new Dictionary<string, int>();
        
        /// <summary>
        /// Abstract builder base class
        /// </summary>
        /// <param name="shader">The compute shader that has all the layer kernels</param>
        protected ABuilder(ComputeShader shader)
        {
            Shader = shader;
        }
        
        /// <summary>
        /// Add a layer to this texture builder
        /// </summary>
        /// <param name="layer">Layer to add</param>
        /// <returns></returns>
        public ABuilder AddLayer<T>(ALayer<T> layer) where T : ABuilder
        {
            layer.Initialize((T)this);
            EnsureKernel(layer.Kernel());
            layer.SetAttributes(GetKernelId(layer.Kernel()));
            DispatchLayer(layer);
            layer.Release();
            return this;
        }
        
        /// <summary>
        /// Dispatch a given kernel
        /// </summary>
        /// <param name="kernel">Target kernel</param>
        /// <param name="threadGroupCountX">Kernel thread group count over X axis</param>
        /// <param name="threadGroupCountY">Kernel thread group count over Y axis</param>
        /// <param name="threadGroupCountZ">Kernel thread group count over Z axis</param>
        public void DispatchKernel(string kernel, int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ)
        {
            Shader.Dispatch(GetKernelId(kernel), threadGroupCountX, threadGroupCountY, threadGroupCountZ);
        }
        
        /// <summary>
        /// Ensures a given kernel is ready
        /// </summary>
        /// <param name="kernel"></param>
        protected void EnsureKernel(string kernel)
        {
            if (!_kernelIds.ContainsKey(kernel))
            {
                _kernelIds.Add(kernel, Shader.FindKernel(kernel));
                InitializeKernel(_kernelIds[kernel]);
            }
        }
        
        /// <summary>
        /// Returns a given kernel's id. Make sure the kernel is ready before calling this method on it
        /// </summary>
        /// <param name="kernel">The kernel you want the id from</param>
        /// <returns></returns>
        private int GetKernelId(string kernel)
        {
            return _kernelIds[kernel];
        }

        /// <summary>
        /// Dispatch a given layer
        /// </summary>
        /// <param name="layer"></param>
        private void DispatchLayer<T>(ALayer<T> layer) where T : ABuilder
        {
            Vector3 threadGroup = layer.ThreadGroup();
            DispatchKernel(layer.Kernel(), (int)threadGroup.x, (int)threadGroup.y, (int)threadGroup.z);
        }
       
        /// <summary>
        /// Compute shader used by this builder
        /// </summary>
        public ComputeShader Shader { get; }
        
        protected abstract void InitializeKernel(int kernel);
        
        /// <summary>
        /// Releases the buffers created by the builder from memory
        /// </summary>
        public abstract void Release();
        
        /// <summary>
        /// The default thread group arrangement used by this builder
        /// </summary>
        /// <returns></returns>
        public abstract Vector3 DefaultThreadGroups();
    }
}
