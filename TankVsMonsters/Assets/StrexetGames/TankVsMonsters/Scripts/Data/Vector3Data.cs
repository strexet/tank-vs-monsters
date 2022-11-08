using System;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Data
{
    [Serializable]
    public class Vector3Data
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(Vector3Data data) => new(data.X, data.Y, data.Z);
        public static implicit operator Vector3Data(Vector3 v) => new(v.x, v.y, v.z);
    }
}