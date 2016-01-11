using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class Nut : MonoBehaviour
    {

        public bool pickedUp = false;

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                if (Input.GetButtonDown("Submit"))
                {
                    Destroy(GetComponent<Rigidbody>());
                    this.gameObject.transform.SetParent(coll.transform);
                    this.gameObject.transform.localPosition = new Vector3(0, 1.5f, 0);
                    pickedUp = true;
                }

            }
        }
    }
}