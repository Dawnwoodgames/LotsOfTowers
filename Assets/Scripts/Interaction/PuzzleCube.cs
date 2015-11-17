using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class PuzzleCube : MonoBehaviour
    {
        public GameObject snapArea;
        private Transform sphere;
        private List<Transform> fractures = new List<Transform>();

        void Start()
        {
            foreach (Transform child in transform)
            {
				child.GetComponent<Rigidbody>().isKinematic = true;
                fractures.Add(child);
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
				foreach (Transform child in fractures)
                {
					Rigidbody childRigid = child.GetComponent<Rigidbody>();

					childRigid.isKinematic = false;
                    child.localScale = new Vector3(0.45f, 0.45f, 0.45f);

					childRigid.AddForce(transform.up * 923, ForceMode.Force);
					childRigid.AddForce(child.forward * 291, ForceMode.Force);

					childRigid.useGravity = true;
				}

				//Disable trigger to add force to fracments
				GetComponent<BoxCollider>().enabled = false;

                StartCoroutine(ActivateFrozenFractures());
            }
        }

        private IEnumerator ActivateFrozenFractures()
        {
            yield return new WaitForSeconds(2);

            snapArea.SetActive(true);
            foreach (Transform child in fractures)
            {
                child.GetComponent<Rigidbody>().mass = 0.1f;
            }
        }
    }
}
