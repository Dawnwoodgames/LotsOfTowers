using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.Interaction.Triggers
{
	public class FractureReset : MonoBehaviour
	{
		public Vector3 resetPosition = new Vector3();
		public float resetIfBlow = 18;
		private Vector3 currentPosition;
		// Update is called once per frame
		void Update()
		{
			if (transform.position.y < resetIfBlow)
			{
				if(!GetComponent<Rigidbody>().isKinematic)
				{
					ResetPosition();
				}
			}
		}

		private void ResetPosition()
		{
			transform.position = resetPosition;
        }
	}
}
