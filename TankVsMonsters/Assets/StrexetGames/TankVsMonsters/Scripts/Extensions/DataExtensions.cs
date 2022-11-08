using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Extensions
{
    public static class DataExtensions
    {
        public static T FromJson<T>(this string json) => JsonUtility.FromJson<T>(json);
        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
    }
}