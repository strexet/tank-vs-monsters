using System;

namespace StrexetGames.TankVsMonsters.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLevel) => WorldData = new WorldData(initialLevel);
    }
}