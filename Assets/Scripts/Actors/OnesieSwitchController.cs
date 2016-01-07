﻿using UnityEngine;

namespace LotsOfTowers.Actors
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class OnesieSwitchController : MonoBehaviour
    {
        private new GameObject camera;
		private GameObject player;

        public void Awake()
        {
            this.camera = GameObject.Find("CenterFocus");
			player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Trigger()
        {
            GetComponent<Animator>().SetTrigger("Switching");
        }

        public void Update()
        {
			//float cameraDistance = Vector3.Distance(Camera.main.transform.position, player.transform.position);
			transform.parent.transform.rotation = camera.transform.localRotation;

            transform.rotation = camera.transform.rotation;
        }
    }
}