using Actors.Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }

            var progressJson = _progressService.Progress.ToJson();
            
            PlayerPrefs.SetString(ProgressKey, progressJson);
            PlayerPrefs.Save();
        }

        public PlayerProgress LoadProgress()
        {
            var progressJson = PlayerPrefs.GetString(ProgressKey);
            var playerProgress = progressJson?.FromJson<PlayerProgress>();
            return playerProgress;
        }
    }
}