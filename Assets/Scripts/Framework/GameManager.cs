using LotsOfTowers.Actors;
using SmartLocalization;
using System;
using System.Linq;
using UnityEngine;

namespace LotsOfTowers.Framework
{
	public class GameManager : MonoBehaviour
	{
		private Player player;
		private Transform spawnPoint;

		public static bool Alive { get { return Instance != null; } }
		public static GameManager Instance { get; private set; }

		public string Language
		{ // Default: en
			get { return PlayerPrefs.HasKey("Language") ? PlayerPrefs.GetString("Language") : "en"; }
			set
			{
				if (LanguageManager.Instance.GetSupportedLanguages().Where(l => l.languageCode == value).Count() != 0)
				{
					LanguageManager.Instance.ChangeLanguage(value);
					PlayerPrefs.SetString("Language", value);
				}
			}
		}
		
		public TagManager ManagedObjects {
			get { return gameObject.GetComponent<TagManager> (); }
		}

		public Transform SpawnPoint {
			get { return spawnPoint; }
		}

		static GameManager()
		{
			Instance = new GameObject("Game Manager", new Type[] {
				typeof(GameManager), typeof(TagManager)
			}).GetComponent<GameManager>();
		}


		public void Awake()
		{
			if (FindObjectsOfType<GameManager>().Length > 1) {
				Destroy(gameObject);
			}

			DontDestroyOnLoad(this);
			LanguageManager.Instance.ChangeLanguage(Language);
			Physics.gravity = new Vector3(0, -35, 0);
		}

		public void OnLevelWasLoaded(int level) {
			if (player == null) {
				// Try to find the player
				player = FindObjectOfType<Player>();
			}

			spawnPoint = GameObject.Find("Level/Spawn Point").transform;
		}
	}
}