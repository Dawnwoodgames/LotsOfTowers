using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class LightOnOff : MonoBehaviour {

        public GameObject[] LightsOn;
        public GameObject[] LightsOff;

        void OnTriggerEnter(Collider col)
        {
            if(col.tag == "Player" || col.tag == "HamsterBall")
            {
                foreach (GameObject light in LightsOn)
                {
                    light.SetActive(true);
                }
                foreach (GameObject light in LightsOff)
                {
                    light.SetActive(false);
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player" || col.tag == "HamsterBall")
            {
                foreach (GameObject light in LightsOn)
                {
                    light.SetActive(false);
                }

                foreach (GameObject light in LightsOff)
                {
                    light.SetActive(true);
                }
            }
        }
    }
}