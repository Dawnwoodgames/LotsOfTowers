using UnityEngine;
using System.Collections;
using Nimbi.Actors;
namespace Nimbi.Interaction
{

    public class ScaryStatue : MonoBehaviour
    {

        public bool isScary;

        private Player player;
        private GameObject scaryStatue;
        private bool inTrigger;



        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            scaryStatue = GameObject.Find("Scary-Statue");
            inTrigger = false;
            isScary = true;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetButtonDown("Submit") && inTrigger && isScary)
            {
                scaryStatue.transform.Rotate(180, 0, 0);
                isScary = false;
            }
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
 