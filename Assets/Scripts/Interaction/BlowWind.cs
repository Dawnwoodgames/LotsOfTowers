using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LotsOfTowers.Actors;
using LotsOfTowers.Framework;


namespace LotsOfTowers.Interaction
{
    public class BlowWind : MonoBehaviour
    {
        public float interval = 0f;
        public float force = 50f;
        public Direction direction = Direction.Forward;

        private Vector3 dir;
        private List<GameObject> collisions = new List<GameObject>();
		
        private bool hasBlock;
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
                    if (isBlowing)
                        windParticles.Play();
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
            }
        }

        private void Wind()
        {
            if (active && isBlowing)
            {
                block.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Acceleration);
                if (!hasBlock)
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
            if (!col.GetComponent<Rigidbody>().isKinematic)
            {
                collisions.Add(col.gameObject);
            }
        }

        void OnTriggerExit(Collider col)
        {
            active = false;
            if (!col.GetComponent<Rigidbody>().isKinematic)
            {
                collisions.Remove(col.gameObject);
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
                default:
                    dir = Vector3.forward;
                    break;
            }

            
        }
        
    }
}

