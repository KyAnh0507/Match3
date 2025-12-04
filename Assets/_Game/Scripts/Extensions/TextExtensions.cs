using UnityEngine;
using TMPro;
//using I2.Loc;

namespace TileCat3.Extensions
{
    public static class TextExtensions
    {
        private static Color color;

        public static void ChangeAlpha(this TMP_Text text, float alpha)
        {
            if (alpha >= 0f && alpha <= 1f)
            {
                color = text.color;
                color.a = alpha;
                text.color = color;
            }
        }

        public static void SetTerm(this TMP_Text text, string term)
        {
            //Localize localize = text.GetComponent<Localize>();
            //if (localize == null)
            //{
            //    Debug.LogError("Get localize component failed");
            //}
            //localize.SetTerm(term);
        }
    }
}
