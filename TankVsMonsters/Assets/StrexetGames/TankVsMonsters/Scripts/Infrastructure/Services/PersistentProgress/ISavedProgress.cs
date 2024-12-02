using StrexetGames.TankVsMonsters.Scripts.Data;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}

	public interface ISavedProgress : ISavedProgressReader
	{
		void UpdateProgress(PlayerProgress progress);
	}
}