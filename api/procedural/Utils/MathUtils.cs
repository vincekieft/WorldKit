// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;

namespace WorldKit.api.procedural.Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// Convert index to position conversion
        /// </summary>
        /// <param name="index"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public static Vector2 IndexToPos(int index, int resolution)
        {
            float division = (float)index / resolution;
            int rounded = (int)division;
            return new Vector2(
                (int)((division - rounded) * resolution),
                rounded
            );
        }
        
        /// <summary>
        /// Position to index conversion
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public static int PosToIndex(Vector2 pos, int resolution)
        {
            return (int)(pos.y * resolution) + (int)pos.x;
        }
    }
}