using Nimbi.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nimbi.Interaction {
	public class TransitionBeacon : MonoBehaviour {

		public int levelIndex;

		public void OnTriggerEnter(Collider coll) {
			if (coll.tag == "Player") {
                UnityAnalytics.CompleteLevel(SceneManager.GetActiveScene().name,Mathf.RoundToInt(GameManager.Instance.levelStart-Time.time));
				GameManager.Instance.LoadLevel(levelIndex);
			}
		}
	}
}