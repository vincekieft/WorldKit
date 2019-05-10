using UnityEngine;
using WorldKit.Procedural.Utils;

namespace WorldKit.src.procedural.Utils
{
    public static class TerrainUtils
    {
        /// <summary>
        /// Apply height map onto unity terrain. Keep in mind that this will write in the terrain data so make a backup before calling this method
        /// </summary>
        /// <param name="terrain">Terrain to apply height map onto</param>
        /// <param name="heights">Heights to apply</param>
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