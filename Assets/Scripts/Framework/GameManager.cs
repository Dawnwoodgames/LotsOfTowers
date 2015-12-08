using LotsOfTowers.Actors;
using LotsOfTowers.UI;
using SmartLocalization;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers
{
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(CanvasRenderer))]
	public class GameManager : MonoBehaviour
	{
		private static GameManager instance;
		private Canvas canvas;
		private Image fader;
		private bool hasStarted;
		private PlayerController playerController;
		private Transform spawnPoint;
		
		public static bool Alive { get { return Instance != null; } }
		public static GameManager Instance {
			get {
				if (instance == null) {
					instance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();
				}
				return instance;
			}
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
		
		public Transform SpawnPoint {
			get { return spawnPoint; }
		}
		
		public void Awake()
		{
			if (FindObjectsOfType<GameManager>().Length > 1) {
				Destroy (gameObject);
			} else {
				GameManager.instance = this;
			}
			
			DontDestroyOnLoad(this);
			LanguageManager.Instance.ChangeLanguage(Language);
			LanguageManager.Instance.name = "Language Manager";
			LanguageManager.Instance.transform.SetParent(transform, false);
			OnLevelWasLoaded(Application.loadedLevel);

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
			LoadLevel(index, false);
		}

		public void LoadLevel(int index, bool forceUnlock) {
			if (index == -1) {
				index = Application.loadedLevel;
			} else if (!forceUnlock && PlayerPrefs.GetInt("bIsLevelAvailable" + index, 0) == 0) {
				return;
			}

			PlayerPrefs.SetInt("bIsLevelAvailable" + index, 1);
			Time.timeScale = 1;
			StopAllCoroutines();
			StartCoroutine(LoadLevelCoroutine(index));
		}

		private IEnumerator LoadLevelCoroutine(int index) {
			if (playerController != null) {
				playerController.enabled = false;
			}

			while (!hasStarted) {
				yield return null;
			}
			
			while (fader.color.a < 0.99f) {
				fader.color = Color.Lerp(fader.color, Color.black, 0.1f);
				yield return null;
			}

			Application.LoadLevel(index);
			yield return new WaitForSeconds(1);
			
			while (fader.color.a > 0.01f) {
				fader.color = Color.Lerp(fader.color, Color.clear, 0.1f);
				yield return null;
			}

			if (playerController != null) {
				playerController.enabled = true;
			}
		}
		
		public void OnLevelWasLoaded(int index) {
			try {
				playerController = FindObjectOfType<PlayerController>();
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
			canvas.sortingOrder = Int16.MaxValue;
			fader.color = Color.clear;
			fader.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
			fader.transform.SetParent(transform, false);
			hasStarted = true;
		}
	}
}