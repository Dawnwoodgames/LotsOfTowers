using System.Collections;
using UnityEngine;

namespace LotsOfTowers.Interaction {
	public abstract class TriggerBehaviour : MonoBehaviour {
		
		public abstract IEnumerator TriggerOn();
		public abstract IEnumerator TriggerOff();
	}
}