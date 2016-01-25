using UnityEngine;
using System.Collections;

namespace Nimbi.Actors
{
    public class LightningDisplay : MonoBehaviour
    {

        public GameObject[] lightning;
        public Player player;
        public float treshold;

        private bool animating;

        void Update()
        {
            if(player.StaticCharge > 0 && !animating)
            {
                foreach (GameObject obj in lightning)
                {
                    obj.GetComponent<Animator>().SetBool("Animating", true);
                    
                }
                lightning[1].GetComponent<Renderer>().enabled = true;
                animating = true;
            }
            if(player.StaticCharge > treshold)
            {
                foreach(GameObject obj in lightning)
                {
                    obj.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (player.StaticCharge <= 0 && animating)
            {
                foreach (GameObject obj in lightning)
                {
                    obj.GetComponent<Animator>().SetBool("Animating", false);
                    obj.GetComponent<Renderer>().enabled = false;
                    animating = false;
                }
            }
        }
    }
}