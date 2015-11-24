using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LotsOfTowers.Actors;
using LotsOfTowers.Framework;


namespace LotsOfTowers.Interaction
{
    public class BlowWind : MonoBehaviour
    {
        public float force = 50f;
        public Direction direction = Direction.Forward;

        private Vector3 dir;
        private List<GameObject> collisions = new List<GameObject>();

        private bool hasPlayer;
        private bool hasBlock;
        private bool active;

        private GameObject block;
        private GameObject player;

        private ParticleSystem windParticles; // We can use this to maybe stop the windparticles at the block?

        void Start()
        {
            DetermineDirection();

            block = GameObject.FindGameObjectWithTag("MovableByWind");
            player = GameObject.FindGameObjectWithTag("Player");

        }
        
        void Update()
        {
            hasPlayer = false;
            hasBlock = false;
            checkCollisions();
            Wind();
        }

        //
        private void checkCollisions()
        {
            foreach (GameObject collision in collisions)
            {
                if(collision.tag == "Player")
                {
                    hasPlayer = true;
                }
                if(collision.tag == "MovableByWind")
                {
                    hasBlock = true;
                }
            }
        }

        private void Wind()
        {
            if (active)
            {
                block.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Acceleration);
                if (!hasBlock)
                {
                    if (player.GetComponent<Player>().IsElephant)
                    {

                    }
                    else
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
            if (direction == Direction.Forward)
            {
                dir = Vector3.forward;
            }
            else
            {
                dir = Vector3.back;
            }
        }
        
    }
}

