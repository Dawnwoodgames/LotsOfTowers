using UnityEngine;

namespace LotsOfTowers {
	public sealed class GameManager : MonoBehaviour {
		private static GameManager instance;
		private Transform spawnPoint;

		public static GameManager Instance {
			get { return instance; }
		}

		public Transform SpawnPoint {
			get { return spawnPoint; }
		}

		public void Awake() {
			DontDestroyOnLoad(this);
			GameManager.instance = this;
			OnLevelWasLoaded(Application.loadedLevel);
		}

		public void OnLevelWasLoaded(int index) {
			if (GameObject.Find("Level") != null) {
				spawnPoint = GameObject.Find("Level/Spawn Point").transform;
			}
		}
	}
}