namespace TileCat3.Extensions
{
    public static class PrimitiveExtensions
    {
        #region INT
        /// <summary>
        /// Return true if value in range (min..max)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange0(this int value, int min, int max)
        {
            return value > min && value < max;
        }

        /// <summary>
        /// Return true if value in range [min..max)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange1(this int value, int min, int max)
        {
            return value >= min && value < max;
        }

        /// <summary>
        /// Return true if value in range (min..max]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange2(this int value, int min, int max)
        {
            return value > min && value <= max;
        }

        /// <summary>
        /// Return true if value in range [min..max]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange3(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
        #endregion

        #region FLOAT
        /// <summary>
        /// Return true if value in range (min..max)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange0(this float value, float min, float max)
        {
            return value > min && value < max;
        }

        /// <summary>
        /// Return true if value in range [min..max)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange1(this float value, float min, float max)
        {
            return value >= min && value < max;
        }

        /// <summary>
        /// Return true if value in range (min..max]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange2(this float value, float min, float max)
        {
            return value > min && value <= max;
        }

        /// <summary>
        /// Return true if value in range [min..max]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange3(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
        #endregion
    }
}
