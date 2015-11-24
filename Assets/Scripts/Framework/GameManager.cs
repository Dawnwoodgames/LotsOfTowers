using LotsOfTowers.Actors;
using SmartLocalization;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using LotsOfTowers.UI;

namespace LotsOfTowers
{
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(CanvasRenderer))]
	public class GameManager : MonoBehaviour
	{
		private Canvas canvas;
		private Image fader;
		private bool hasStarted;
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

		static GameManager() {
			try {
				GameManager.Instance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();
			} catch (UnityException) { }
		}
		
		public void Awake()
		{
			if (FindObjectsOfType<GameManager>().Length > 1) {
				Destroy (gameObject);
			} else {
				GameManager.Instance = this;
			}
			
			DontDestroyOnLoad(this);
			LanguageManager.Instance.ChangeLanguage(Language);
			LanguageManager.Instance.name = "Language Manager";
			LanguageManager.Instance.transform.SetParent(transform, false);
			OnLevelWasLoaded(Application.loadedLevel);
			Physics.gravity = new Vector3(0, -35, 0);

			this.canvas = GetComponent<Canvas>();
			this.fader = new GameObject ("Transition Fader", typeof(Image)).GetComponent<Image> ();
		}

		public void FadeIn() {
			StopAllCoroutines();
			StartCoroutine(FadeInCoroutine());
		}

		private IEnumerator FadeInCoroutine() {
			while (!hasStarted) {
				yield return null;
			}

			while (fader.color.a > 0.01f) {
				fader.color = Color.Lerp(fader.color, Color.clear, 0.1f);
				yield return null;
			}
		}

		public void FadeOut() {
			StopAllCoroutines();
			StartCoroutine(FadeOutCoroutine());
		}

		private IEnumerator FadeOutCoroutine() {
			while (!hasStarted) {
				yield return null;
			}

			while (fader.color.a < 0.99f) {
				fader.color = Color.Lerp(fader.color, Color.black, 0.1f);
				yield return null;
			}
		}

		public void LoadLevel(int index) {
			StopAllCoroutines();
			StartCoroutine(LoadLevelCoroutine(index));
		}

		private IEnumerator LoadLevelCoroutine(int index) {
			while (!hasStarted) {
				yield return null;
			}
			
			while (fader.color.a < 0.99f) {
				fader.color = Color.Lerp(fader.color, Color.black, 0.1f);
				yield return null;
			}

			Application.LoadLevel(index);
			
			while (fader.color.a > 0.01f) {
				fader.color = Color.Lerp(fader.color, Color.clear, 0.1f);
				yield return null;
			}
		}
		
		public void OnLevelWasLoaded(int index) {
			try {
				player = FindObjectOfType<Player>();
				spawnPoint = GameObject.Find("Level/Spawn Point").transform;
			} catch (Exception) { }
		}


		public void Quit() {
			StopAllCoroutines();
			StartCoroutine(QuitCoroutine());
		}

		private IEnumerator QuitCoroutine() {
			while (!hasStarted) {
				yield return null;
			}
				
			while (fader.color.a < 0.99f) {
				fader.color = Color.Lerp (fader.color, Color.black, 0.1f);
				yield return null;
			}

			Application.Quit ();
		}

		public UITooltip ShowTooltip(String resourceName) {
			if (PlayerPrefs.GetInt ("bTooltipBeenShown" + resourceName) != 1) {
				PlayerPrefs.SetInt("bTooltipBeenShown" + resourceName, 1);
				UITooltip tooltip = new GameObject ("UITooltip", typeof(UITooltip)).GetComponent<UITooltip> ();
				tooltip.duration = 5;
				tooltip.gameObject.transform.SetParent (transform, false);
				tooltip.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Textures/" + resourceName);

				return tooltip;
			}

			return null;
		}

		public void Start() {
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			fader.color = Color.clear;
			fader.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
			fader.transform.SetParent(transform, false);
			hasStarted = true;
		}
	}
}