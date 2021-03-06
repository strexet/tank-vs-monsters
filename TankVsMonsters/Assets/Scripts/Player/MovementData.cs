using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "Game/MovementData", order = 0)]
    public class MovementData : ScriptableObject
    {
        public float ForwardSpeed;
        public float RotationSpeed;
    }
}