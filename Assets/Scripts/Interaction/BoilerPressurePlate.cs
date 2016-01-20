using UnityEngine;
using System.Collections;
using Nimbi.Actors; 

namespace Nimbi.Interaction
{
    public class BoilerPressurePlate : MonoBehaviour
    {
        public GameObject BoilerLid;
        public int maxLidPosition = 50;

        private bool inTrigger;
        private Vector3 lidPosition; 
        private Player player;
        private bool lifting;
        private Quaternion lidRotation;

        private Vector3 rotateAngle;
        
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            BoilerLid = GameObject.Find("Lid");

            //Old Position values
            lidRotation = transform.rotation;
            
            inTrigger = false;
            lifting = false;
        }

        // Update is called once per frame
        void Update()
        {
            //If Nimbi is In his Elephant Onesie!
            if (inTrigger == true && player.GetComponent<Player>().Onesie.isHeavy)
            {
                lifting = true;

                if (lifting)
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
                    lifting = false;
            }
            }
        
        public void OnTriggerExit()
        {
            inTrigger = false;
        }
  
     }
}

