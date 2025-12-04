using UnityEngine;

namespace TileCat3.Extensions
{
    public static class TransformExtensions
    {
        private static Vector2 tempV2;
        private static Vector3 tempV3;

        #region TRANSFORM
        /// <summary>
        /// Change transform.position.x value
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void ChangePositionX(this Transform transform, float x)
        {
            tempV3 = transform.position;
            tempV3.x = x;
            transform.position = tempV3;
        }

        /// <summary>
        /// Change transform.position.y value
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void ChangePositionY(this Transform transform, float y)
        {
            tempV3 = transform.position;
            tempV3.y = y;
            transform.position = tempV3;
        }

        /// <summary>
        /// Change transform.position.z value
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void ChangePositionZ(this Transform transform, float z)
        {
            tempV3 = transform.position;
            tempV3.z = z;
            transform.position = tempV3;
        }

        /// <summary>
        /// Change transform.localPosition.x value
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="x"></param>
        public static void ChangeLocalPositionX(this Transform transform, float x)
        {
            tempV3 = transform.localPosition;
            tempV3.x = x;
            transform.localPosition = tempV3;
        }

        /// <summary>
        /// Change transform.localPosition.y value
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="y"></param>
        public static void ChangeLocalPositionY(this Transform transform, float y)
        {
            tempV3 = transform.localPosition;
            tempV3.y = y;
            transform.localPosition = tempV3;
        }

        /// <summary>
        /// Change transform.localPosition.z value
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="z"></param>
        public static void ChangeLocalPositionZ(this Transform transform, float z)
        {
            tempV3 = transform.localPosition;
            tempV3.z = z;
            transform.localPosition = tempV3;
        }

        public static void ChangeRotationX(this Transform transform, float x)
        {
            tempV3 = transform.eulerAngles;
            tempV3.x = x;
            transform.eulerAngles = tempV3;
        }

        public static void ChangeRotationY(this Transform transform, float y)
        {
            tempV3 = transform.eulerAngles;
            tempV3.y = y;
            transform.eulerAngles = tempV3;
        }

        public static void ChangeRotationZ(this Transform transform, float z)
        {
            tempV3 = transform.eulerAngles;
            tempV3.z = z;
            transform.eulerAngles = tempV3;
        }

        public static void ChangeScaleX(this Transform transform, float x)
        {
            tempV3 = transform.localScale;
            tempV3.x = x;
            transform.localScale = tempV3;
        }

        public static void ChangeScaleY(this Transform transform, float y)
        {
            tempV3 = transform.localScale;
            tempV3.y = y;
            transform.localScale = tempV3;
        }

        public static void ChangeScaleZ(this Transform transform, float z)
        {
            tempV3 = transform.localScale;
            tempV3.z = z;
            transform.localScale = tempV3;
        }

        /// <summary>
        /// Destroy all children of an object
        /// </summary>
        /// <param name="transform"></param>
        public static void ClearAllChildren(this Transform transform)
        {
            if (transform.childCount == 0) return;

            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Destroy all children of an object in edit mode
        /// </summary>
        /// <param name="transform"></param>
        public static void ClearAllChildrenEditMode(this Transform transform)
        {
            if (transform.childCount == 0) return;

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }
        #endregion

        #region RECT TRANSFORM
        public static void ChangeAnchorPosX(this RectTransform rt, float x)
        {
            tempV2 = rt.anchoredPosition;
            tempV2.x = x;
            rt.anchoredPosition = tempV2;
        }

        public static void ChangeAnchorPosY(this RectTransform rt, float y)
        {
            tempV2 = rt.anchoredPosition;
            tempV2.y = y;
            rt.anchoredPosition = tempV2;
        }

        public static void ChangeLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void ChangeRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void ChangeTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void ChangeBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
        #endregion
    }
}
