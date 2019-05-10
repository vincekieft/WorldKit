using UnityEngine;
using WorldKit.Procedural.Layers;
using WorldKit.Procedural.Structs;
using WorldKit.Procedural.Utils;

namespace WorldKit.Procedural.Builders
{
    public class MeshBuilder : ABuilder
    {
        private readonly Vector3 _extends;
        private readonly int _vertexResolution;
        private readonly ComputeBuffer _vertexBuffer;
        
        public MeshBuilder(ComputeShader shader, int resolution, Vector3 extends) : this(shader, BufferUtils.CreateVertexBuffer(resolution), extends) {}
        public MeshBuilder(ComputeShader shader, VertexStruct[] vertices, Vector3 extends) : this(shader, BufferUtils.CreateVertexBuffer(vertices), extends) {}
        public MeshBuilder(ComputeShader shader, ComputeBuffer vertexBuffer, Vector3 extends) : base(shader)
        {
            _extends = extends;
            _vertexResolution = (int)Mathf.Sqrt(vertexBuffer.count);
            _vertexBuffer = vertexBuffer;
        }
        
        protected override void InitializeKernel(int kernel)
        {
            Shader.SetBuffer(kernel, Constants.VertexBuffer, _vertexBuffer);
            Shader.SetInt(Constants.VertexCountAttribute,_vertexBuffer.count);
            Shader.SetVector(Constants.MeshExtendsAttribute, _extends);
            Shader.SetInt(Constants.VertexResolutionAttribute, _vertexResolution);
        }

        public override void Release()
        {
            _vertexBuffer.Release();
        }

        public override Vector3 DefaultThreadGroups()
        {
            return new Vector3(Mathf.CeilToInt((float)_vertexBuffer.count / Constants.GpuGridSize), 1, 1);
        }

        public VertexStruct[] GetVertices()
        {
            VertexStruct[] vertices = new VertexStruct[_vertexBuffer.count];
            _vertexBuffer.GetData(vertices);
            return vertices;
        }
    }
}