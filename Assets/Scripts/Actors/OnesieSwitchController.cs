using UnityEngine;

namespace Nimbi.Actors
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class OnesieSwitchController : MonoBehaviour
    {
        private new GameObject camera;
		//private GameObject player;

        public void Awake()
        {
            this.camera = GameObject.Find("CenterFocus");
			//player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Trigger()
        {
            GetComponent<Animator>().SetTrigger("Switching");
        }

        public void Update()
        {
			transform.parent.transform.rotation = camera.transform.localRotation;

            transform.rotation = camera.transform.rotation;
        }
    }
}