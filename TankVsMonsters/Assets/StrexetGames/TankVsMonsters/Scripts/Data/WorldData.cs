using System;

namespace StrexetGames.TankVsMonsters.Scripts.Data
{
	[Serializable]
	public class WorldData
	{
		public PositionOnLevel PlayerPositionOnLevel;

		public WorldData(string initialLevel) => PlayerPositionOnLevel = new PositionOnLevel(initialLevel);
	}
}