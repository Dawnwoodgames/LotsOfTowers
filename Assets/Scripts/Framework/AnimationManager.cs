using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nimbi.Framework
{
    public class AnimationManager : MonoBehaviour
    {
        public AnimationClip[] animationsClips; // We make an Array we can add our custom Animations in!

        public enum AnimationStates //With An Enum we can check in a Switch on what State we need an Animation!
        {
            Special,
            Idle,
            Interacting
              
        }

        private AnimationStates myState; //This is a Private Variable we will Use to check the State in our Animation!
        private bool inTrigger;

        void Start()
        {
            //Empty at The Moment
            myState = AnimationStates.Idle;
        }

        // Update is called once per frame
        void Update()
        {

          

            foreach (AnimationClip ac in animationsClips)
            {
                //We are checking each Animation (Added in the Editor) to what state they belong!
                if (Input.GetButtonDown("Submit") && inTrigger)
                {
                    Debug.Log("I Try to Interact With Something!");
                    whatToAnimate(myState);
                }
                
            }
        }


        //A public function we create to check our Enum States and attach the name of the animation!
        public void whatToAnimate(AnimationStates aS)
        {
            switch (aS)
            {
                case AnimationStates.Idle:
                    GetComponent<Animation>().Play(animationsClips[0].name);
                    break;
                case AnimationStates.Interacting:
                    GetComponent<Animation>().Play(animationsClips[1].name);
                    break;
                default:
                    //Lets do nothing default for now!
                    break;
            }
        }


        //If our Object gets an Interaction with our Player(with his Tag Player)
        public void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                inTrigger = true;
                Debug.Log("Rigid Attached!");
            }
        }

        public void OnTriggerExit()
        {
            Debug.Log("Ik ben de trigger uit!");
            inTrigger = false;
        }

    }

}

