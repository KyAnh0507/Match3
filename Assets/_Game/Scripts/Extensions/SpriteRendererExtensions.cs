using UnityEngine;

namespace TileCat3.Extensions
{
    public static class SpriteRendererExtensions
    {
        private static Color color;

        public static void ChangeAlpha(this SpriteRenderer sr, float alpha)
        {
            if (alpha >= 0f && alpha <= 1f)
            {
                color = sr.color;
                color.a = alpha;
                sr.color = color;
            }
        }
    }
}
