using StrexetGames.TankVsMonsters.Scripts.Data;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}