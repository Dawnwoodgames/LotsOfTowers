using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class PuzzleCube : MonoBehaviour
    {
        public GameObject snapArea;
        private Transform floor;
        private Transform sphere;
        private List<Transform> fractures = new List<Transform>();

        void Start()
        {
            floor = GameObject.Find("FloatingFloor").transform;
            foreach (Transform child in floor)
            {
                fractures.Add(child);
            }
        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                foreach (Transform child in fractures)
                {
                    child.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                    child.GetComponent<Rigidbody>().AddForce(transform.up * 800, ForceMode.Force);
                    child.GetComponent<Rigidbody>().AddForce(child.forward * 120, ForceMode.Force);
                    child.GetComponent<Rigidbody>().useGravity = true;
                }
                Destroy(GetComponent<BoxCollider>());

                StartCoroutine(ActivateFrozenFractures());
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                snapArea.SetActive(true);
                foreach (Transform child in fractures)
                {
                    child.GetComponent<Rigidbody>().mass = 0.1f;
                    child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                }
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                foreach (Transform child in fractures)
                {
                    child.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                    child.GetComponent<Rigidbody>().AddForce(transform.up * 800, ForceMode.Force);
                    child.GetComponent<Rigidbody>().AddForce(child.forward * 120, ForceMode.Force);
                    child.GetComponent<Rigidbody>().useGravity = true;
                }
                Destroy(GetComponent<BoxCollider>());

                StartCoroutine(ActivateFrozenFractures());
            }
        }

        private IEnumerator ActivateFrozenFractures()
        {
            yield return new WaitForSeconds(1);

            snapArea.SetActive(true);
            foreach (Transform child in fractures)
            {
                child.GetComponent<Rigidbody>().mass = 0.1f;
                child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
