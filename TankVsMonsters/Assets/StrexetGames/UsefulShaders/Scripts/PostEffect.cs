using UnityEngine;

namespace StrexetGames.UsefulShaders
{
    [ExecuteInEditMode]
    public class PostEffect : MonoBehaviour
    {
        [SerializeField] private Material _material;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            // Parameter "src" is fully rendered scene that you would normally send directly to the monitor.
            // We are intercepting this so we can do a bit more work, before passing it on.

            Graphics.Blit(src, dest, _material);
        }
    }
}

