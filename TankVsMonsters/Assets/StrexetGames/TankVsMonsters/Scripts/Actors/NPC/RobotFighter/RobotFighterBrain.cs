using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.NPC
{
    public class RobotFighterBrain : MonoBehaviour, IAiBrain
    {
        public Vector2 MovementAxis { get; }
        public bool IsAttackButtonPressed { get; }
    }
}