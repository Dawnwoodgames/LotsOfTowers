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
		
		public Transform SpawnPoint {
			get { return spawnPoint; }
		}
		
		
		public void Awake()
		{
			if (FindObjectsOfType<GameManager> ().Length > 1) {
				Destroy (gameObject);
			} else {
				GameManager.Instance = this;
			}
			
			DontDestroyOnLoad(this);
			LanguageManager.Instance.ChangeLanguage(Language);
			OnLevelWasLoaded(Application.loadedLevel);
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