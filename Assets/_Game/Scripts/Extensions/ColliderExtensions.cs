//using TileCat3.Cache;
using UnityEngine;

namespace TileCat3.Extensions
{
    public static class ColliderExtensions
    {
        /*public static T GetComponentFromCache<T>(this Collider collider)
        {
            return CacheCollider<T>.Get(collider);
        }

        public static T GetComponentFromCache<T>(this Collider2D collider2D)
        {
            return CacheCollider2D<T>.Get(collider2D);
        }*/

        public static void ChangeBoxColliderCenterX(this BoxCollider boxCollider, float x)
        {
            Vector3 temp = boxCollider.center;
            temp.x = x;
            boxCollider.center = temp;
        }

        public static void ChangeBoxColliderCenterY(this BoxCollider boxCollider, float y)
        {
            Vector3 temp = boxCollider.center;
            temp.y = y;
            boxCollider.center = temp;
        }

        public static void ChangeBoxColliderCenterZ(this BoxCollider boxCollider, float z)
        {
            Vector3 temp = boxCollider.center;
            temp.z = z;
            boxCollider.center = temp;
        }

        public static void ChangeBoxColliderSizeX(this BoxCollider boxCollider, float x)
        {
            Vector3 temp = boxCollider.size;
            temp.x = x;
            boxCollider.size = temp;
        }

        public static void ChangeBoxColliderSizeY(this BoxCollider boxCollider, float y)
        {
            Vector3 temp = boxCollider.size;
            temp.y = y;
            boxCollider.size = temp;
        }

        public static void ChangeBoxColliderSizeZ(this BoxCollider boxCollider, float z)
        {
            Vector3 temp = boxCollider.size;
            temp.z = z;
            boxCollider.size = temp;
        }
    }
}
