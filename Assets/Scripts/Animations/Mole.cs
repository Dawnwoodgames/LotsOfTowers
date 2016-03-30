using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class Mole : MonoBehaviour
    {

        //What does our Gameobject has for Animation States?
        public enum AnimationState
        {
            DiggingUp, IsTalking
        }


        //Private Booleans for encountering counters!
        private bool firstEncounter;
        private bool inTrigger;

        //Create an instance of our Animation Enum so we can set our states to an enum value!
        private AnimationState getState;

  
        void Start()
        {
            firstEncounter = false;
        }

        //Update Magic
        void Update()
        {
            //What if we are in a colliderbox and we have not had any interaction with our mole?
            if (inTrigger && firstEncounter == false)
            {
                Debug.Log("InTrigger werkt!");
                GetComponent<Animation>().Play("DiggingUp");
                getState = AnimationState.DiggingUp; //Change our enum to GettingUp (Digging)
                firstEncounter = true;
            }
        }



        public void OnTriggerEnter(Collider coll)
        {
            if (coll.attachedRigidbody)
            {
                inTrigger = true;
            }
        }

    }

}
