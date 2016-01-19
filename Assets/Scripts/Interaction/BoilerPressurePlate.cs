using UnityEngine;
using System.Collections;
using Nimbi.Actors; 

namespace Nimbi.Interaction
{
    public class BoilerPressurePlate : MonoBehaviour
    {

        private bool colliding;
        private GameObject player;
        // Use this for initialization

        void Start()
        {
             
        }

        // Update is called once per frame
        void Update()
        {
            if(colliding == true)
            {
                print("Nimbi is standing on me");
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (colliding || collision.gameObject.tag != "Player") return;
            colliding = true;
        }
    }
}

