using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Physics;
using UnityEngine;

namespace Logic
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