using UnityEngine;

namespace WorldKit.Procedural.Utils
{
    public static class TerrainUtils
    {
        public static void ApplyHeightMap(this UnityEngine.Terrain terrain, float[] heights)
        {
            int resolution = (int)Mathf.Sqrt(heights.Length);
            float[,] unityHeights = new float[resolution, resolution];

            for (int i = 0; i < heights.Length; i++)
            {
                Vector2 pos = MathUtils.IndexToPos(i, resolution);
                unityHeights[(int)pos.x, (int)pos.y] = heights[i];
            }
            
            terrain.terrainData.SetHeights(0,0,unityHeights);
        }
    }
}