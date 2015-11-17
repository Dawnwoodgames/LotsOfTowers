using UnityEngine;
using System.Collections.Generic;

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
			//sphere = GameObject.Find("Sphere").transform;
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
				//sphere.GetComponent<Rigidbody>().AddForce(transform.up * 1200, ForceMode.Force);
				foreach (Transform child in fractures)
				{
                    child.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    child.GetComponent<Rigidbody>().AddForce(transform.up * 800, ForceMode.Force);
                    child.GetComponent<Rigidbody>().AddForce(child.forward * 120, ForceMode.Force);
                    child.GetComponent<Rigidbody>().useGravity = true;
                }
            }
			if (Input.GetKeyDown(KeyCode.V))
			{
                snapArea.SetActive(true);
				foreach (Transform child in fractures)
				{
					child.GetComponent<Rigidbody>().mass = 0.1f;
					child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				}

				//Destroy(sphere.gameObject);
			}
		}

        void OnTriggerEnter(Collider coll)
        {
            foreach (Transform child in fractures)
            {
                child.GetComponent<Rigidbody>().AddForce(transform.up * 800, ForceMode.Force);
                child.GetComponent<Rigidbody>().AddForce(child.forward * 120, ForceMode.Force);
                child.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        }
}
