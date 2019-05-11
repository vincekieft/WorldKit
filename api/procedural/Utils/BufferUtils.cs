// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Structs;

namespace WorldKit.api.procedural.Utils
{
    public static class BufferUtils
    {
        /// <summary>
        /// Creates a new RenderTexture instance
        /// </summary>
        public static RenderTexture CreateRenderTexture(int resolution)
        {
            RenderTexture texture = new RenderTexture(resolution, resolution, 24)
            {
                enableRandomWrite = true
            };
            texture.Create();
            return texture;
        }

        /// <summary>
        /// Creates a new height buffer
        /// </summary>
        /// <param name="resolution">Resolution of vertex buffer. Resulting vertex count is: resolution * resolution</param>
        /// <returns></returns>
        public static ComputeBuffer CreateVertexBuffer(int resolution)
        {
            return CreateVertexBuffer(new VertexStruct[resolution * resolution]);
        }
        
        /// <summary>
        /// Creates a new height buffer
        /// </summary>
        /// <param name="vertices">Input vertices</param>
        /// <returns></returns>
        public static ComputeBuffer CreateVertexBuffer(VertexStruct[] vertices)
        {
            int vertexStride = (sizeof(float) * 3) * 2; // Calculate vertex stride length. Vector3 * 2 for vertex pos and vertex normal
            ComputeBuffer buffer = new ComputeBuffer(vertices.Length, vertexStride);
            buffer.SetData(vertices);
            return buffer;
        }
        
        /// <summary>
        /// Creates a new height buffer
        /// </summary>
        /// <param name="resolution">resolution of the height buffer over x and y axis</param>
        /// <returns></returns>
        public static ComputeBuffer CreateHeightBuffer(int resolution)
        {
            return CreateHeightBuffer(new float[resolution * resolution]);
        }

        /// <summary>
        /// Creates a new height buffer
        /// </summary>
        /// <param name="heights">Input floats</param>
        /// <returns></returns>
        public static ComputeBuffer CreateHeightBuffer(float[] heights)
        {
            ComputeBuffer buffer = new ComputeBuffer(heights.Length, sizeof(float));
            buffer.SetData(heights);
            return buffer;
        }
    }
}