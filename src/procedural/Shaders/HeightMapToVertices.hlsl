// Structs
struct Vertex{
    float3 vertex;
    float3 normal;
};

// Variables
uint VertexCount;
uint VertexResolution;
RWStructuredBuffer<Vertex> VertexBuffer;
float3 MeshExtends;

// Kernel
[numthreads(8,1,1)]
void HeightMapToVertices (uint3 id : SV_DispatchThreadID)
{
    // Check wheter index falls within the vertex count
    if(id.x < VertexCount){
        
        // Calcualte relative mesh pos
        uint2 pos = IndexToVector(id.x, VertexResolution);
        float2 relativePos = RelativizeVector(pos.x, pos.y, VertexResolution, VertexResolution);
        
        // Get vertex from vertex buffer
        float3 vertex = float3(relativePos.x * MeshExtends.x, 0, relativePos.y * MeshExtends.z);
        
        // Calculate vertex relative to heightmap
        float3 relativeVertex = RelativizeVector(vertex, MeshExtends);
        float2 heightMapVertex = float2(relativeVertex.x * HeightMapResolution, relativeVertex.z * HeightMapResolution);
        
        // Calculate height
        vertex.y = CalculateGradientAndHeight(heightMapVertex, HeightMapResolution, HeightBuffer).z * MeshExtends.y;
        
        // Set vertex height
        VertexBuffer[id.x].normal = CalculateNormal(pos, HeightMapResolution, MeshExtends, HeightBuffer);
        VertexBuffer[id.x].vertex = vertex;
    }
}