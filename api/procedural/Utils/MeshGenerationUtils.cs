// Licensed under the Non-Profit Open Software License version 3.0

using System;
using UnityEngine;
using WorldKit.api.procedural.Structs;

namespace WorldKit.api.procedural.Utils
{
    public static class MeshGenerationUtils
    {
        public static Mesh[] VerticesToMeshChunks(VertexStruct[] vertices, int chunkResolution)
        {
            chunkResolution = Mathf.Max(chunkResolution, CalculateTerrainChunkResolution(vertices.Length));
            
            Mesh[] meshes = new Mesh[chunkResolution * chunkResolution];

            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i] = VerticesToChunk(vertices, i, chunkResolution);
            }
            
            return meshes;
        }

        public static Mesh VerticesToChunk(VertexStruct[] vertices, int chunk, int chunkResolution)
        {
            int resolution = (int)Mathf.Sqrt(vertices.Length);
            Vector2 chunkPos = MathUtils.IndexToPos(chunk, chunkResolution);
            float chunkDivision = (float)resolution / chunkResolution;
            int roundedChunkDivision = Mathf.CeilToInt(chunkDivision);
            
            int posX = (int)chunkPos.x * roundedChunkDivision;
            int posZ = (int)chunkPos.y * roundedChunkDivision;
            

            int width = (int)(Mathf.Clamp((chunkPos.x + 1) * roundedChunkDivision + 1, 0, resolution) - posX);
            int height = (int)(Mathf.Clamp((chunkPos.y + 1) * roundedChunkDivision + 1, 0, resolution) - posZ);
            
            Vector3[] meshVertices = new Vector3[width * height];
            Vector3[] vertexNormals = new Vector3[width * height];
            int[] meshTriangles = new int[CalculateGridTriangleCount(width, height) * 3];
            
            int vIndex = 0;
            int tIndex = 0;
            
            for (int z = 0; z < height; z++)
            {
                int rowProgress = z * width;
                
                for (int x = 0; x < width; x++)
                {
                    // Create triangles
                    if (x < width - 1 && z < height - 1)
                    {
                        // Bottom open triangle
                        meshTriangles[tIndex++] = rowProgress + x; // Top left
                        meshTriangles[tIndex++] = rowProgress + x + width; // Bottom left
                        meshTriangles[tIndex++] = rowProgress + x + 1; // Top right
                        
                        // Top open triangle
                        meshTriangles[tIndex++] = rowProgress + x + 1; // Top right
                        meshTriangles[tIndex++] = rowProgress + x + width; // Bottom left
                        meshTriangles[tIndex++] = rowProgress + x + width + 1; // Bottom right
                    }

                    Vector2 vertexIndex = new Vector2(posX + x, posZ + z);

                    int i = MathUtils.PosToIndex(vertexIndex, resolution);

                    if (i >= vertices.Length)
                    {
                        MonoBehaviour.print(vertexIndex + " / "+resolution);
                        MonoBehaviour.print(i);
                    } else if (vIndex >= meshVertices.Length)
                    {
                        MonoBehaviour.print("here");
                    }
                    
                    vertexNormals[vIndex] = vertices[MathUtils.PosToIndex(vertexIndex, resolution)].Normal;
                    meshVertices[vIndex++] = vertices[MathUtils.PosToIndex(vertexIndex, resolution)].Vertex;
                }
            }

            Mesh m = new Mesh();
            
            m.vertices = meshVertices;
            m.triangles = meshTriangles;
            m.normals = vertexNormals;
            
            return m;
        }
        
        /// <summary>
        /// Calculate chunk resolution of terrain mesh.
        /// For example: a chunk resolution of 2 means that the terrain should be divided up into 2 by 2 chunks (4 chunks in total)
        /// </summary>
        /// <param name="vertexCount"></param>
        /// <returns></returns>
        public static int CalculateTerrainChunkResolution(int vertexCount)
        {
            return Mathf.CeilToInt(Mathf.Sqrt((float)vertexCount / Constants.MaxMeshVertexCount));
        }
        
        /// <summary>
        /// Generates a plane mesh
        /// </summary>
        /// <param name="rowVertexCount">The amount of vertices per row in the plane. Total vertex count is: rowVertexCount * rowVertexCount</param>
        /// <param name="planeSize">The world size of the resulting plane. X axis represents width and Y axis represents depth</param>
        /// <returns></returns>
        /// <exception cref="Exception">When the generated vertex count is higher then 65000 an exception is thrown</exception>
        /// <exception cref="Exception">rowVertexCount must atleast be one (1), cannot be zero (0)</exception>
        public static Mesh GeneratePlaneMesh(int rowVertexCount, Vector2 planeSize)
        {
            int vertexCount = CalculateGridVertexCount(rowVertexCount, rowVertexCount);
        
            // Header guards
            if (vertexCount > Constants.MaxMeshVertexCount){ throw new Exception("Plane vertex count exceeds the limit of 65000. Vertex count: "+vertexCount); }
            if (rowVertexCount <= 0){ throw new Exception("rowVertexCount is zero (0). rowVertexCount must atleast be one (1)"); }

            Mesh planeMesh = new Mesh();
            Vector2 quadSize = planeSize / rowVertexCount;
            Vector3[] vertices = new Vector3[vertexCount];
            Vector3[] normals = new Vector3[vertexCount];
            int[] triangles = new int[CalculateGridTriangleCount(rowVertexCount, rowVertexCount) * 3];

            int vIndex = 0;
            int tIndex = 0;
        
            for (int z = 0; z < rowVertexCount; z++)
            {
                int rowProgress = z * rowVertexCount;
            
                for (int x = 0; x < rowVertexCount; x++)
                {
                    // Set vertex
                    vertices[vIndex].x = x * quadSize.x;
                    vertices[vIndex].z = z * quadSize.y;
                
                    // Set normal
                    normals[vIndex].x = 0;
                    normals[vIndex].y = 1;
                    normals[vIndex].z = 0;

                    // Create triangles
                    if (x < rowVertexCount - 1 && z < rowVertexCount - 1)
                    {
                        // Bottom open triangle
                        triangles[tIndex++] = rowProgress + x; // Top left
                        triangles[tIndex++] = rowProgress + x + rowVertexCount; // Bottom left
                        triangles[tIndex++] = rowProgress + x + 1; // Top right
                    
                        // Top open triangle
                        triangles[tIndex++] = rowProgress + x + 1; // Top right
                        triangles[tIndex++] = rowProgress + x + rowVertexCount; // Bottom left
                        triangles[tIndex++] = rowProgress + x + rowVertexCount + 1; // Bottom right
                    }
                
                    vIndex++;
                } 
            }

            planeMesh.vertices = vertices;
            planeMesh.triangles = triangles;
            planeMesh.normals = normals;
        
            return planeMesh;
        }
   
        /// <summary>
        /// Calculates the vertex count of a grid based on the column count
        /// </summary>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static int CalculateGridVertexCount(int columns, int rows)
        {
            return columns * rows;
        }

        /// <summary>
        /// Calculates the amount of quads in a grid based on the column count
        /// </summary>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static int CalculateGridQuadCount(int columns, int rows)
        {
            return CalculateGridVertexCount(columns - 1, rows - 1);
        }
    
        /// <summary>
        /// Calculates the triangle count of a grid plane based on the column count
        /// </summary>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static int CalculateGridTriangleCount(int column, int rows)
        {
            return CalculateGridQuadCount(column, rows) * 2;
        }
    }
}
