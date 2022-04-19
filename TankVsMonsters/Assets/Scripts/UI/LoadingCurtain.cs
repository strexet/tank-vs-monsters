using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _fadeSpeed = 0.03f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            _curtain.alpha = 1;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            StartCoroutine(FadeOutRoutine());
        }

        private IEnumerator FadeOutRoutine()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _fadeSpeed;
                yield return null;
            }

            gameObject.SetActive(false);
        }

        private void OnValidate()
        {
            if (_fadeSpeed < 0.0001f)
                _fadeSpeed = 0.0001f;
        }
    }
}