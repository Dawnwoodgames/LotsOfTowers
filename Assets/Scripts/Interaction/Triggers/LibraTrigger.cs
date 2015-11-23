using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class LibraTrigger : MonoBehaviour
    {

        public GameObject libra;
        public bool playerOnLibra = false;
        public bool elephantReadyToLaunch = false;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player" && libra.transform.rotation.x >= -13 && !playerOnLibra)
            {
                playerOnLibra = true;
                this.transform.localPosition = new Vector3(-10.49f, -5f, 1f);
                libra.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (coll.tag == "Player" && libra.transform.rotation.x >= 13 && playerOnLibra)
            {
                elephantReadyToLaunch = true;
            }
        }
    }
}