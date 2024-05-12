using StrexetGames.TankVsMonsters.Scripts.Actors.NPC.Common;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.NPC.RobotFighter
{
    public class RobotFighterBrain : MonoBehaviour, IAiBrain
    {
        public Vector2 MovementAxis { get; }
        public bool IsAttackButtonPressed { get; }
    }
}