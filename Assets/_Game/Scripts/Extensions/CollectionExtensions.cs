using System.Linq;
using System.Collections.Generic;

namespace TileCat3.Extensions
{
    public static class CollectionExtensions
    {
        private static System.Random random = new System.Random();

        #region ARRAY & LIST
        /// <summary>
        /// Shuffle an array or a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        public static void Shuffle<T>(this IList<T> ts)
        {
            int i = ts.Count;
            int j;

            while (i > 1)
            {
                i--;
                j = random.Next(i + 1);
                T t = ts[j];
                ts[j] = ts[i];
                ts[i] = t;
            }
        }

        public static T Last<T>(this IList<T> ts)
        {
            return ts[ts.Count - 1];
        }

        public static void Swap<T>(this IList<T> ts, int i1, int i2)
        {
            if (i1 < 0 || i1 > ts.Count - 1 || i2 < 0 || i2 > ts.Count - 1)
            {
                UnityEngine.Debug.LogError("Invalid index");
                return;
            }

            T temp = ts[i1];
            ts[i1] = ts[i2];
            ts[i2] = temp;
        }

        public static T Random<T>(this IList<T> ts)
        {
            return ts[UnityEngine.Random.Range(0, ts.Count)];
        }

        public static void Rotate<T>(this List<T> list, int positions)
        {
            int count = list.Count;
            positions = positions % count;

            if (positions == 0) return;

            if (positions < 0) positions += count;

            list.Reverse();
            list.Reverse(0, positions);
            list.Reverse(positions, count - positions);
        }

        public static List<T> Clone<T>(this List<T> list)
        {
            List<T> newList = new List<T>();

            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(list[i]);
            }

            return newList;
        }
        #endregion

        #region DICTIONARY
        public static Dictionary<TKey, TValue> DeepClone<TKey, TValue>(this Dictionary<TKey, TValue> origin)
        {
            var clone = origin.ToDictionary(e => e.Key, e => e.Value);
            return clone;
        }
        #endregion
    }
}
