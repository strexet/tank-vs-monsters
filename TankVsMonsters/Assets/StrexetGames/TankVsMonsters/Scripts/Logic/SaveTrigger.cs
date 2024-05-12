using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.SaveLoad;
using StrexetGames.TankVsMonsters.Scripts.Physics;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        private ISaveLoadService _saveLoad;

        private void Awake() => _saveLoad = ServiceLocator.Container.Single<ISaveLoadService>();

        private void OnEnable() => _triggerObserver.TriggerEnter += TriggerSave;

        private void OnDisable() => _triggerObserver.TriggerEnter -= TriggerSave;

        private void TriggerSave(Collider obj)
        {
            _saveLoad.SaveProgress();
            Debug.Log($"[SAVED] <color=green>{nameof(SaveTrigger)}.{nameof(TriggerSave)}></color> Progress Saved!");
            gameObject.SetActive(false);
        }
    }
}