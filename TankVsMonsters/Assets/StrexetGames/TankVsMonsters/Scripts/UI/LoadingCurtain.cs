using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _fadeTime = 1f;

        private void Awake() => DontDestroyOnLoad(this);

        public void Show()
        {
            _curtain.alpha = 1;
            gameObject.SetActive(true);
        }

        public void Hide() =>
            LeanTween.alphaCanvas(_curtain, 0, _fadeTime)
               .setEase(LeanTweenType.easeOutQuad)
               .setOnComplete(_ => gameObject.SetActive(false));
    }
}