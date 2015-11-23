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

        private float blockMass;

        private Vector3 dir;
        private List<GameObject> collisions = new List<GameObject>();

        private bool hasPlayer;
        private bool hasBlock;

        private GameObject block;
        private GameObject player;
        
        void Start()
        {
            blockMass = GameObject.Find("MovableByWindBlockOne").GetComponent<Rigidbody>().mass;
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
            block.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Acceleration);
        }


        void OnTriggerEnter(Collider col)
        {
            if (!col.GetComponent<Rigidbody>().isKinematic)
            {
                collisions.Add(col.gameObject);
            }
        }

        void OnTriggerExit(Collider col)
        {
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

