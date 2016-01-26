﻿using UnityEngine;
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
        private Vector3 standardMode;
      
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            inTrigger = false;
            isScary = true;
            standardMode = new Vector3();

            

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetButtonDown("Submit") && inTrigger && isScary)
            {

                //Rotate the Statue a certain row of times till it is not scary anymore!
                scaryStatue.transform.Rotate(standardMode.z += 0, 0, 20);

                if (scaryStatue.transform.rotation.eulerAngles.z == 180)
                {
                    //If the statue show his happy face, the dragon will not be scared anymore.
                    isScary = false;
                    print("Doet het!");
                }
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
 