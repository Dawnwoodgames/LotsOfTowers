using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class LibraTrigger : MonoBehaviour
    {
        public GameObject libra;
        private GameObject elephant;
        public bool playerOnLibra = false;
        public bool elephantReadyToLaunch = false;

        private void Start()
        {
            elephant = GameObject.Find("NpcElephant");
        }

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player" && libra.transform.rotation.x >= -13 && !playerOnLibra)
            {
                playerOnLibra = true;
                this.transform.localPosition = new Vector3(-19f, transform.localPosition.y, transform.localPosition.z);
                libra.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (coll.tag == "Player" && elephantReadyToLaunch && playerOnLibra)
            {
                Destroy(elephant.GetComponent<NavMeshAgent>());
                elephant.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}