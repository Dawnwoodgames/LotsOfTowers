using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nimbi.Framework
{
    public class AnimationManager : MonoBehaviour
    {    
        public bool firstInteraction;
        public bool hasQuestHelp;

        public AnimationClip[] animationsClips; // We make an Array we can add our custom Animations in!
        public enum AnimationStates //With An Enum we can check in a Switch on what State we need an Animation!
        {
            Special, //Special is Default name for now (Mole digs up)
            Idle,
            Interacting

        }

        private AnimationStates myState; //This is a Private Variable we will Use to check the State in our Animation!
        private bool inTrigger;
        private bool hasSingleAnimation;

        void Start()
        {
            //Default state is Idle State
            myState = AnimationStates.Idle;
        }

        // Update is called once per frame
        void Update()
        {

    
            
            //We are checking each Animation (Added in the Editor) to what state they belong!
            if (Input.GetButtonDown("Submit") && inTrigger)
            {          
                myState = AnimationStates.Interacting;
                whatToAnimate(myState);
                Debug.Log(myState);
            }

        }

        //A public function we create to check our Enum States and attach the name of the animation!
        public void whatToAnimate(AnimationStates aS)
        {

            for (int i = 0; i < animationsClips.Length; i++)
            {        
                if(i > animationsClips.Length)
                {
                    //Get Default Animation
                    GetComponent<Animation>().Play(animationsClips[0].name);
                    Debug.Log("No more Animations to Show!");
                }

                switch (aS)
                {
                    case AnimationStates.Idle:
                        i++;
                        GetComponent<Animation>().Play(animationsClips[i].name);
                        break;
                    case AnimationStates.Interacting:
                        GetComponent<Animation>().Play(animationsClips[i].name);
                        break;
                    default:
                        GetComponent<Animation>().Play(animationsClips[i].name);
                        break;
                }
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
            myState = AnimationStates.Idle;
            inTrigger = false;
        }

    }

}

