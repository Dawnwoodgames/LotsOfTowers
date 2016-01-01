using UnityEngine;

namespace LotsOfTowers.Actors {
	[RequireComponent(typeof(Animator))]
	public class Skeleton : MonoBehaviour {
		private Animator animator;
		private SkinnedMeshRenderer renderer;

		public Animator Animator {
			get { return animator; }
		}

		public SkinnedMeshRenderer Renderer {
			get { return renderer; }
		}

		public void Awake() {
			animator = GetComponent<Animator>();
			renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		}
	}
}