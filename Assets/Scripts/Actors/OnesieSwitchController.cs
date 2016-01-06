using UnityEngine;

namespace LotsOfTowers.Actors
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
            this.transform.rotation = camera.transform.rotation;
        }
    }
}