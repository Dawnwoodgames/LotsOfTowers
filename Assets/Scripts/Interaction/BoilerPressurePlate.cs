using UnityEngine;
using System.Collections;
using Nimbi.Actors; 

namespace Nimbi.Interaction
{
    public class BoilerPressurePlate : MonoBehaviour
    {
        public GameObject BoilerLid;
        public int maxLidPosition = 50;
        public bool lidIsOpen;

        private Player player;
        private bool inTrigger;
      
        private Vector3 rotateAngle;
        private Vector3 lidPosition; 
        private Quaternion lidRotation;

        
        
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            BoilerLid = GameObject.Find("Lid");

            //Old Position values
            lidRotation = transform.rotation;
            
            inTrigger = false;
            lidIsOpen = false;
        }

        // Update is called once per frame
        void Update()
        {
            //If Nimbi is In his Elephant Onesie!
            if (inTrigger == true && player.GetComponent<Player>().Onesie.isHeavy)
            {
                lidIsOpen = true;

                if (lidIsOpen)
                {
                    //Do Stuff
                    Vector3 endPosition = new Vector3(-17, 0, 0);
                    if (Vector3.Distance(BoilerLid.transform.eulerAngles, endPosition) > 0.01f)
                    {
                        if (BoilerLid.transform.eulerAngles.z < maxLidPosition)
                        BoilerLid.transform.Rotate(-16, 0, 17);
                    }

                    else
                    {
                        BoilerLid.transform.Rotate(+17, 0, 0);
                        print("Not Lifting Anymore");
                    }

                }
            }
        }

        public void OnTriggerStay(Collider col)
            {
                if (col.attachedRigidbody)
                {
                    inTrigger = true;
                    lidIsOpen = false;
            }
            }
        
        public void OnTriggerExit()
        {
            inTrigger = false;
        }
  
     }
}

