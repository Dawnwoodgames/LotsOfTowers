using LotsOfTowers.Actors;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Framework
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;
		private Actor actor;
		private string[] languages;
		private Transform spawnPoint;
		private float timeScale;

		public static GameManager Instance
		{
			get { return instance; }
		}

		public string Language
		{ // Default: en_US
			get { return PlayerPrefs.HasKey("Language") ? PlayerPrefs.GetString("Language") : "en_US"; }
			set { if (languages.Contains(value)) { PlayerPrefs.SetString("Language", value); } }
		}

		public bool Paused
		{
			get { return Time.timeScale == 0; }
			set { timeScale = Paused ? timeScale : Time.timeScale; Time.timeScale = value ? 0 : timeScale; }
		}

		public Transform SpawnPoint
		{
			get { return spawnPoint; }
		}

		public void Awake()
		{
			DontDestroyOnLoad(this);
			GameManager.instance = this;
			OnLevelWasLoaded(Application.loadedLevel);
			this.actor = FindObjectOfType<Actor>();
			this.languages = new string[] { "en_US", "nl_NL" };
			this.timeScale = Time.timeScale;
		}

		public void OnLevelWasLoaded(int index)
		{
			if (GameObject.Find("Level") != null)
			{
				spawnPoint = GameObject.Find("Level/Spawn Point").transform;
				if (actor != null && spawnPoint != null) {
					actor.transform.position = spawnPoint.position;
				}
			}
		}
	}
}