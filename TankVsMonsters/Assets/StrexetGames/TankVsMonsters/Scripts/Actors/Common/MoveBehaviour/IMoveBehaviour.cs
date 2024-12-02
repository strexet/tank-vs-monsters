using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Actors.Common.MoveBehaviour
{
	public interface IMoveBehaviour
	{
		Vector3 CurrentPosition { get; set; }

		void UpdateMoveInput(Vector2 movementAxis);

		void Move(MovementData movementData);
	}
}