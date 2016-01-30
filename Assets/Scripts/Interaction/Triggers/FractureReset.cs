using UnityEngine;
using System.Collections;
using System;

namespace Nimbi.Interaction.Triggers
{
	public class FractureReset : MonoBehaviour
	{
		public Vector3 resetPosition = new Vector3();
		public float resetIfBelow = -0.1f;
		private Vector3 currentPosition;
		// Update is called once per frame
		void Update()
		{
			if (transform.localPosition.y < resetIfBelow)
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
