using UnityEngine;

namespace LotsOfTowers.Framework {
	public class TagBundle : MonoBehaviour {

		// Tags
		public bool Movable = false;

		public void Awake() { GameManager.Instance.ManagedObjects.Add(this); }
		public void OnDestroy() { if (GameManager.Alive) GameManager.Instance.ManagedObjects.Remove(this); }
	}
}