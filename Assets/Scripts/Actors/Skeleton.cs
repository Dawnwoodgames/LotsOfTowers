using UnityEngine;

namespace LotsOfTowers.Actors {
	[RequireComponent(typeof(Animator))]
	public sealed class Skeleton : MonoBehaviour {
		private Animator animator;
		private SkinnedMeshRenderer renderer;

		public Animator Animator {
			get { return animator; }
		}

		public SkinnedMeshRenderer Renderer {
			get { return renderer; }
		}

		public void Awake() {
			this.animator = GetComponent<Animator>();
			this.renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		}
	}
}