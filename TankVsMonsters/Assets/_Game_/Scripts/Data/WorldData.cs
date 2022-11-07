using System;

namespace Actors.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PlayerPositionOnLevel;

        public WorldData(string initialLevel) => PlayerPositionOnLevel = new PositionOnLevel(initialLevel);
    }
}