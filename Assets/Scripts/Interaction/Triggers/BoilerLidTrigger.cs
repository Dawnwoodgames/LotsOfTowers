using UnityEngine;
using System.Collections;
using Nimbi.Actors;


namespace Nimbi.Interaction.Triggers
{
    public class BoilerLidTrigger : MonoBehaviour
    {
     
        public int maxLidPosition = 50;
        public GameObject BoilerLid;
        float rotationLidSpeed = 1.0f;

        private BoilerPressurePlate lidOpen; 
        private Vector3 rotateAngle;
        private Vector3 lidPosition;
        private Quaternion lidRotation;


        void Start()
        {
            lidOpen = GameObject.Find("PressurePlate").GetComponent<BoilerPressurePlate>();
            lidRotation = transform.rotation;
        }

        void Update()
        {
            if (lidOpen.lidIsOpen)
            {

                Vector3 endPosition = new Vector3(-17, 0, 0);
                if (Vector3.Distance(BoilerLid.transform.eulerAngles, endPosition) > 0.01f)
                {
                    if (BoilerLid.transform.eulerAngles.z < maxLidPosition)
                        BoilerLid.transform.Rotate(-16, 0, 17);

                }
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lidRotation, Time.time * rotationLidSpeed);
            }
        }
    }
}

