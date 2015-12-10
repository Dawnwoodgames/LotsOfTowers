using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class WaterHole : MonoBehaviour
    {
        public HamsterWheel wheel;
        public GameObject bridge;
        public bool waterRising = true;

        private int raiseAmount = 0;
        
        void Update()
        {
            if (wheel.GetRotateSpeed() > 14 && waterRising)
                transform.localScale += new Vector3(0, 1 * Time.deltaTime, 0);

            if (bridge.transform.position.y < (transform.position.y + (transform.localScale.y / 2)))
                bridge.transform.Translate(new Vector3(0, 1f * Time.deltaTime));
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.gameObject.name == "hamsterwheel")
            {
                transform.Translate(new Vector3(0, .1f * Time.deltaTime));
                if (raiseAmount < 240)
                    raiseAmount++;

                if (raiseAmount >= 240 && bridge.transform.position.y >= (transform.position.y + (transform.localScale.y / 2)))
                    Destroy(this);
            }
        }
    }
}