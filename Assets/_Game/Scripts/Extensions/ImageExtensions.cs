using UnityEngine;
using UnityEngine.UI;

namespace TileCat3.Extensions
{
    public static class ImageExtensions
    {
        private static Color color;

        /// <summary>
        /// Change image.color.a value
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void ChangeAlpha(this Image image, float alpha)
        {
            if (alpha >= 0f && alpha <= 1f)
            {
                color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }
}
