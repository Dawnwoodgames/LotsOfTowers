using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Framework
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;
		private string[] languages;
		private Transform spawnPoint;

		public static GameManager Instance
		{
			get { return instance; }
		}

		public string Language
		{ // Default: en_US
			get { return PlayerPrefs.HasKey("Language") ? PlayerPrefs.GetString("Language") : "en_US"; }
			set { if (languages.Contains(value)) { PlayerPrefs.SetString("Language", value); } }
		}

		public Transform SpawnPoint
		{
			get { return spawnPoint; }
		}

		public void Awake()
		{
			DontDestroyOnLoad(this);
			GameManager.instance = this;
			languages = new string[] { "en_US", "nl_NL"};
			OnLevelWasLoaded(Application.loadedLevel);
		}

		public void OnLevelWasLoaded(int index)
		{
			if (GameObject.Find("Level") != null)
			{
				spawnPoint = GameObject.Find("Level/Spawn Point").transform;
			}
		}
	}
}