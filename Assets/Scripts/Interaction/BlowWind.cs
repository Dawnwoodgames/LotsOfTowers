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
        private bool active = false;

        private Vector3 dir;
        private List<GameObject> collisions = new List<GameObject>();

        private bool hasBlock = false;
        private bool hasPlayer = false;

        void Start()
        {
            blockMass = GameObject.Find("MovableByWindBlockOne").GetComponent<Rigidbody>().mass;

            DetermineDirection();

        }


        void Update()
        {
            Wind();
        }

       

        private void Wind()
        {
            foreach (GameObject collision in collisions)
            {
                CheckCollisions(collision);

                
                if (hasPlayer && collision.GetComponent<Player>().IsElephant)
                {
                    // Player can Push it.
                    GameObject.Find("MovableByWindBlockOne").GetComponent<Rigidbody>().mass = 0.1f;
                    collision.GetComponent<Rigidbody>().AddForce(dir * force / 5, ForceMode.Acceleration);
                }
                else
                {
                    GameObject.Find("MovableByWindBlockOne").GetComponent<Rigidbody>().mass = blockMass;
                    collision.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Acceleration);
                }

            }
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
                Debug.Log(col.name);
                collisions.Remove(col.gameObject);
            }
        }

        // Check the collisions and set the right variables to the right values
        private void CheckCollisions(GameObject c)
        {
            if (c.tag == "Player")
            {
                hasPlayer = true;
            }
            else
            {
                hasPlayer = false;
            }

            if (c.tag == "MovableByWind")
            {
                hasBlock = true;
            }
            else
            {
                hasBlock = false;
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

