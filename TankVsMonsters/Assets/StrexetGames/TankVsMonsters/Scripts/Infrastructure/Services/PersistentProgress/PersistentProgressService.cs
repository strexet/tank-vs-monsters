using StrexetGames.TankVsMonsters.Scripts.Data;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}

	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}