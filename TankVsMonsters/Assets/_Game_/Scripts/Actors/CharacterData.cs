using UnityEngine;

namespace Actors.Player
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Game/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public float Height;
    }
}