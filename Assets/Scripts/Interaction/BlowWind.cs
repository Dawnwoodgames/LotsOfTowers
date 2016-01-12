using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Nimbi.Actors;
using Nimbi.Framework;


namespace Nimbi.Interaction
{
    public class BlowWind : MonoBehaviour
    {
        public float interval = 0f;
        public float force = 50f;
        public Direction direction = Direction.Forward;
		public bool hasIceBlock = false;

		private Vector3 dir;
        private List<GameObject> collisions = new List<GameObject>();
		
        private bool hasBlock = false;
		private bool hasPlayer = false;
		private bool active;

        private GameObject block;
        private GameObject player;
        private bool isBlowing = true;
        private float nextBlow;

        public ParticleSystem windParticles; // We can use this to maybe stop the windparticles at the block?

		void Start()
        {
            DetermineDirection();
            nextBlow = Time.time;
            block = GameObject.FindGameObjectWithTag("MovableByWind");
            player = GameObject.FindGameObjectWithTag("Player");

        }
        
        void FixedUpdate()
        {
            CheckBlowing();
            hasBlock = false;
			hasPlayer = false;
			hasIceBlock = false;
            checkCollisions();
            Wind();
        }

        private void CheckBlowing()
        {
            if (interval > 0)
            {
                if (Time.time > nextBlow)
                {
                    isBlowing = !isBlowing;
                    windParticles.loop = isBlowing;
					if (isBlowing && !hasIceBlock)
					{
						windParticles.Play();
					}
					else if (hasIceBlock)
					{
						active = false;
						windParticles.loop = false;
						windParticles.Stop();
                    }
					nextBlow = Time.time + interval;
                }
            }
        }

        //
        private void checkCollisions()
        {
            foreach (GameObject collision in collisions)
            {
                if(collision.tag == "MovableByWind")
                {
                    hasBlock = true;
                }

				if (collision.tag == "IceBlock")
				{
					hasIceBlock = true;
				}

				if (collision.tag == "Player")
				{
					hasPlayer = true;
				}
            }
        }

        private void Wind()
        {
            if (active && isBlowing && !hasIceBlock)
            {
				if (block != null)
				{
					block.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Acceleration);
				}

                if (!hasBlock && hasPlayer)
                {
                    if (!player.GetComponent<Player>().Onesie.isHeavy)
                    {
						player.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Acceleration);
                    }
                }
            }
        }


        void OnTriggerStay(Collider col)
        {
            active = true;
            if (!col.GetComponent<Rigidbody>().isKinematic || col.tag == "IceBlock")
            {
				if(!collisions.Contains(col.gameObject))
				{
					collisions.Add(col.gameObject);
				}
			}
        }

        void OnTriggerExit(Collider col)
        {
            active = false;
            if (!col.GetComponent<Rigidbody>().isKinematic)
            {
				if (collisions.Contains(col.gameObject))
				{
					collisions.Remove(col.gameObject);
				}
            }
        }

        // Sets the direction the wind is blowing
        private void DetermineDirection()
        {
            switch (direction)
            {
                case Direction.Forward:
                    dir = Vector3.forward;
                    break;
                case Direction.Backward:
                    dir = Vector3.back;
                    break;
                case Direction.Left:
                    dir = Vector3.left;
                    break;
                case Direction.Right:
                    dir = Vector3.right;
                    break;
				case Direction.Up:
					dir = Vector3.up;
					break;
				case Direction.Down:
					dir = Vector3.down;
					break;
                default:
                    dir = Vector3.forward;
                    break;
            }
        }
    }
}

