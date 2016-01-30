using UnityEngine;
using System.Collections;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    public class WaterHole : MonoBehaviour
    {
        public GameObject rotateTrigger;
        public GameObject bridge;
        public float raiseAmount = 240;
        public bool waterRising = true;
        public float growToScale = 2.3f;

        public RotateTrigger rotateTriggerScript;
        private int raised = 0;

        private bool raiseWaterFromHamsterWheel = false;

        void FixedUpdate()
        {
            if (rotateTriggerScript.GetPlayerRunning() && waterRising)
            {
                transform.localScale += new Vector3(0, 1 * Time.deltaTime / 5, 0);
            }
                

            if(transform.localScale.y >= growToScale)
            {
                waterRising = false;
            }


            if (bridge.transform.position.y < (transform.position.y + (transform.localScale.y / 2)))
            {
                bridge.transform.Translate(new Vector3(0, 1 * Time.deltaTime / 5, 0));
            }
               
            
            if(raiseWaterFromHamsterWheel)
            {
                if (raised < raiseAmount)
                {
                    raised++;
                    transform.Translate(new Vector3(0, .1f * Time.deltaTime));
                }

                if (raised >= raiseAmount && bridge.transform.position.y >= (transform.localPosition.y + (transform.localScale.y / 2)))
                {
                    Destroy(this);
                    raiseWaterFromHamsterWheel = false;
                }
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.name == "hamsterwheel")
            {
                raiseWaterFromHamsterWheel = true;
            }
        }
    }
}