using UnityEngine;

namespace Nimbi.Actors
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class OnesieSwitchController : MonoBehaviour
    {
        private new GameObject camera;

        public void Awake()
        {
            this.camera = GameObject.Find("CenterFocus");
        }

        public void Trigger()
        {
            GetComponent<Animator>().SetTrigger("Switching");
        }

        public void Update()
        {
            if (camera != null) {
                transform.parent.transform.rotation = camera.transform.localRotation;
                transform.rotation = camera.transform.rotation;
            }
        }
    }
}