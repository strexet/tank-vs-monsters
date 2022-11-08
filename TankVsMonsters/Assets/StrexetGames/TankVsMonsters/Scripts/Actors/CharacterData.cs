using UnityEngine;

namespace _Game_.Scripts.Actors
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public float Height;
    }
}