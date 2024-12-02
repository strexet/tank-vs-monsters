using StrexetGames.TankVsMonsters.Scripts.Actors.NPC.Common;
using UnityEngine;
using UsefulTools.Runtime.DataStructures.InterfaceImplementations;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.NPC.RobotFighter
{
	public class RobotFighterBrain : MonoBehaviourImplementation<IAiBrain>, IAiBrain
	{
		public Vector2 MovementAxis { get; }

		public bool IsAttackButtonPressed { get; }
	}
}