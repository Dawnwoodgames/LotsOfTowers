using System.Collections;
using UnityEngine;

namespace LotsOfTowers.Interaction {
	public abstract class TriggerBehaviour : MonoBehaviour {
		
		public abstract IEnumerator TriggerOn(GameObject source);
		public abstract IEnumerator TriggerOff(GameObject source);
	}
}