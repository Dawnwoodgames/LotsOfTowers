using UnityEngine;
using System.Collections;
using Nimbi;

namespace Nimbi.Interaction.Triggers
{
    public class DoorOpenTrigger : MonoBehaviour
    {

        public GameObject[] doors;

      

        private Quaternion startRotation;
        private Quaternion endRotation;

        public float degreesPerSecond, rotationDegreesAmount;
        private float totalRotation;

        private bool inTrigger;

        // Use this for initialization
        void Start()
        {
            startRotation = new Quaternion(0, 0, 0, 1);
            endRotation = new Quaternion(0, 0, 0, 1);
        }

        // Update is called once per frame
        void Update()
        {
            if (inTrigger)
            {
                OpenDoors();
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if(coll.tag == "Player")
            {
                inTrigger = true;
            }
        }

        void OpenDoors()
        {
            foreach(GameObject d in doors)
            {
                float currentAngle = d.transform.rotation.eulerAngles.y;
                d.transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * degreesPerSecond), Vector3.up);
                totalRotation += Time.deltaTime * degreesPerSecond;
            }

           
        }
    }

}