// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;

namespace WorldKit.api.procedural.Utils
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

            Vector2 pos = Vector2.zero;
            
            for (int i = 0; i < heights.Length; i++)
            {
                unityHeights[(int)pos.x, (int)pos.y] = heights[i];

                if (pos.x < resolution - 1)
                {
                    pos.x += 1;
                }
                else
                {
                    pos.x = 0;
                    pos.y += 1;
                }
                
            }
            
            terrain.terrainData.SetHeights(0,0,unityHeights);
            
        }
    }
}