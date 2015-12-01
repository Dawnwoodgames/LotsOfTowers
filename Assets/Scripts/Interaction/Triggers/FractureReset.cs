using UnityEngine;
using System.Collections;
using System;

namespace LotsOfTowers.Interaction.Triggers
{
	public class FractureReset : MonoBehaviour
	{
		public Vector3 resetPosition = new Vector3();

		// Update is called once per frame
		void Update()
		{
			if (transform.position.y > -1f)
			{
				ResetPosition();
			}
		}

		private void ResetPosition()
		{
			transform.position = resetPosition;
        }
	}
}
