using LotsOfTowers.Actors;
using System.Linq;
using UnityEngine;
using SmartLocalization;

namespace LotsOfTowers.Framework
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;
		private Actor actor;
		private Transform spawnPoint;
		private float timeScale;

		public static GameManager Instance
		{
			get { return instance; }
		}

		public string Language
		{ // Default: en
			get { return PlayerPrefs.HasKey("Language") ? PlayerPrefs.GetString("Language") : "en"; }
			set {
				if (LanguageManager.Instance.GetSupportedLanguages().Where(l => l.languageCode == value).Count() != 0) {
					LanguageManager.Instance.ChangeLanguage(value);
					PlayerPrefs.SetString("Language", value);
				}
			}
		}

		public bool Paused
		{ // Default: false
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
			LanguageManager.Instance.ChangeLanguage(Language);
			OnLevelWasLoaded(Application.loadedLevel);
			this.actor = FindObjectOfType<Actor>();
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