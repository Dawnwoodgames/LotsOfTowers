using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
	public class MarbleCompleteTrigger : MonoBehaviour
	{
		public bool puzzleCompleted { get; set; }

		private void OnTriggerEnter(Collider coll)
		{
			if (coll.name == "Marble")
			{
				Complete();
			}
		}

		private void Complete()
		{
			puzzleCompleted = true;
		}
	}
}