using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LotsOfTowers.Interaction
{
    public class PuzzleCube : MonoBehaviour
    {
		public GameObject snapArea;
		public GameObject triggerBrickPuzzle;
		private bool brickPuzzleComplete = false;

		void Start()
        {
            foreach (Transform child in transform)
            {
				child.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

		private void Update()
		{
			if(!brickPuzzleComplete)
			{
				if (GetComponentsInChildren<Rigidbody>().All(ri => ri.isKinematic == true) && triggerBrickPuzzle.GetComponent<BoxCollider>().enabled == false)
				{
					snapArea.SetActive(false);
					brickPuzzleComplete = true;
					ActivateFloatingFloor();
                }
			}
		}

		private void OnCollisionEnter(Collision col)
		{
			if(col.gameObject.tag == "Player")
			{
				col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			}
		}

		private void OnCollisionExit(Collision col)
		{
			if (col.gameObject.tag == "Player")
			{
				col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
			}
		}
        
		private void ActivateFloatingFloor()
		{
			foreach (Transform child in transform)
			{
				child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			}

			Rigidbody ri = GetComponent<Rigidbody>();
			GetComponent<BoxCollider>().enabled = true;
			ri.isKinematic = false;
			ri.useGravity = true;
        }
    }
}
