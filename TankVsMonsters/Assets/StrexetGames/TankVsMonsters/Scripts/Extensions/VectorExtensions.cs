using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 AddX(this Vector3 v, float x) => new(v.x + x, v.y, v.z);
        public static Vector3 AddY(this Vector3 v, float y) => new(v.x, v.y + y, v.z);
        public static Vector3 AddZ(this Vector3 v, float z) => new(v.x, v.y, v.z + z);

        public static Vector3 XZ(this Vector3 v, float x) => new(v.x, 0f, v.z);
    }
}