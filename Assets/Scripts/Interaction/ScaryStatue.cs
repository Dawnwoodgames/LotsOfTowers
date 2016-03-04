using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.UI;

namespace Nimbi.Interaction
{
    public class ScaryStatue : MonoBehaviour
    {

        public float maxRotationAngle = 180;
        private Player player;
        public GameObject scaryStatue;
        private bool inTrigger = false;
        private Vector3 goalRotation;


        public bool isScary { get; set; }

        void Start()
        {
            isScary = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            goalRotation = transform.localRotation.eulerAngles;
        }

        void Update()
        {
            if (Input.GetButtonDown("Submit") && inTrigger && isScary && player.GetComponent<Player>().Onesie.isHeavy)
            {
                //Rotate the Statue a certain row of times till it is not scary anymore!
                goalRotation += new Vector3(0, 0, 30);
            }
            if (scaryStatue.transform.localRotation.eulerAngles.z >= 180)
            {
                goalRotation.z = 180;
                if (!GameObject.Find("CenterFocus").GetComponent<OnesieInfoPopup>().IsPopupShowing(OnesieType.Dragon))
                {
                    //If the statue show his happy face, the dragon will not be scared anymore.
                    isScary = false;
                }
            }
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(goalRotation), Time.deltaTime);
        }
        private void OnTriggerStay(Collider coll)
        {
            if (coll.attachedRigidbody)
            {
                inTrigger = true;
            }
        }

    }
}






