using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class WaterHole : MonoBehaviour
    {
        public HamsterWheel wheel;
        public bool waterRising = true;

        private int raiseAmount = 0;
        
        void Update()
        {
            if (wheel.GetRotateSpeed() > 14 && waterRising)
                transform.localScale += new Vector3(0, 1 * Time.deltaTime, 0);
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.gameObject.name == "hamsterwheel")
            {
                transform.Translate(new Vector3(0, .1f * Time.deltaTime));
                raiseAmount++;
                if (raiseAmount > 240) Destroy(this);
            }
        }
    }
}