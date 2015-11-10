using LotsOfTowers.Actors;
using System.Linq;
using UnityEngine;
using SmartLocalization;

namespace LotsOfTowers.Framework
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;

		private Player actor;

		private Transform spawnPoint;
		private float timeScale;

		public static GameManager Instance
		{
			get { return instance; }
		}

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
			instance = this;
			LanguageManager.Instance.ChangeLanguage(Language);
			OnLevelWasLoaded(Application.loadedLevel);

			actor = FindObjectOfType<Player>();

			timeScale = Time.timeScale;

			//Set gravity for entire game
			Physics.gravity = new Vector3(0, -35.0F, 0);
		}

		public void OnLevelWasLoaded(int index)
		{
			if (GameObject.Find("Level") != null)
			{
				spawnPoint = GameObject.Find("Level/Spawn Point").transform;
				if (actor != null && spawnPoint != null)
				{
					actor.transform.position = spawnPoint.position;
				}
			}
		}
	}
}