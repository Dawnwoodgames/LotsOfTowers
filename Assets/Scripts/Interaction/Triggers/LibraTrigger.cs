using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class LibraTrigger : MonoBehaviour
    {

        public GameObject libra;
        public bool firstTrigger = false;
        public bool secondTrigger = false;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player" && libra.transform.rotation.x >= -13 && !firstTrigger)
            {
                firstTrigger = true;
                this.transform.localPosition = new Vector3(-10.49f, -5f, 1f);
            }
            if (coll.tag == "Player" && libra.transform.rotation.x >= 13 && firstTrigger)
            {
                secondTrigger = true;
            }
        }
    }
}