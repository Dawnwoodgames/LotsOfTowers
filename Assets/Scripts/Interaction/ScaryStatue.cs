using UnityEngine;
using System.Collections;
using Nimbi.Actors;
namespace Nimbi.Interaction
{

    public class ScaryStatue : MonoBehaviour
    {

        public bool isScary;
        public float maxRotationAngle = 180;
        private Player player;
        public GameObject scaryStatue;
        private bool inTrigger;
        private Vector3 goalRotation;

      
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inTrigger = false;
            isScary = true;
            goalRotation = transform.localRotation.eulerAngles;
            

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Submit") && inTrigger && isScary)
            {

                //Rotate the Statue a certain row of times till it is not scary anymore!
                goalRotation += new Vector3(0, 0, 30);

                
            }
            if (scaryStatue.transform.localRotation.eulerAngles.z >= 180)
            {
                //If the statue show his happy face, the dragon will not be scared anymore.
                isScary = false;
                goalRotation.z = 180;
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
 