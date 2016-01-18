using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class LightOnOff : MonoBehaviour {

        public GameObject[] Lights;

        void OnTriggerEnter(Collider col)
        {
            foreach(GameObject light in Lights)
            {
                light.SetActive(true);
            }
        }

        void OnTriggerExit(Collider col)
        {
            foreach (GameObject light in Lights)
            {
                light.SetActive(false);
            }
        }
    }
}