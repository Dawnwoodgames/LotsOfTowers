using Nimbi.Actors;
using Nimbi.Interaction;
using Nimbi.CameraControl;
using Nimbi.UI;
using SmartLocalization;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nimbi
{
	[RequireComponent(typeof(Canvas))]
	[RequireComponent(typeof(CanvasRenderer))]
	[RequireComponent(typeof(CanvasScaler))]
	public class GameManager : MonoBehaviour
	{
		public const float FadeDuration = 0.5f;

		private static GameManager instance;
		private Canvas canvas;
		private CanvasScaler canvasScaler;
		private Image fader;
		private bool hasStarted;
		private Image loadingScreen;
		private Sprite loadingSpriteA, loadingSpriteB;
		private Player player;
		private PlayerController playerController;
		private Transform spawnPoint;

		public static GameManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GameObject("Game Manager", typeof(GameManager)).GetComponent<GameManager>();
				}
				return instance;
			}
		}

		public bool CursorEnabled
		{
			get { return Cursor.lockState == CursorLockMode.None && Cursor.visible; }
			set
			{
				if (!Debug.isDebugBuild)
				{
					Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
					Cursor.visible = value;
				}
			}
		}

		public bool JoystickConnected
		{
			get { return Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames().Where(s => s != String.Empty).Count() > 0; }
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

		public bool LoadingScreenVisible
		{
			get { return loadingScreen != null && loadingScreen.enabled == true; }
		}

		public Transform SpawnPoint
		{
			get; set;
		}

		public void Awake()
		{
			if (FindObjectsOfType<GameManager>().Length > 1)
			{
				Destroy(gameObject);
			}
			else
			{
				GameManager.instance = this;
			}

			AudioListener.volume = PlayerPrefs.GetFloat("AudioVolume", 1);
			DontDestroyOnLoad(this);
			Instantiate(Resources.Load("Prefabs/AudioManager"));
			LanguageManager.Instance.ChangeLanguage(Language);
			LanguageManager.Instance.name = "Language Manager";
			LanguageManager.Instance.transform.SetParent(transform, false);
			OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);

			this.canvas = GetComponent<Canvas>();
			this.canvasScaler = GetComponent<CanvasScaler>();
			this.fader = new GameObject("Transition Fader", typeof(Image)).GetComponent<Image>();
			this.loadingScreen = new GameObject("Loading Screen", typeof(Image)).GetComponent<Image>();
			this.loadingSpriteA = Resources.Load<Sprite>("UI/LoadingScreenLoading");
			this.loadingSpriteB = Resources.Load<Sprite>("UI/LoadingScreenDone");
		}

		public void FadeIn()
		{
			StopAllCoroutines();
			StartCoroutine(FadeInCoroutine());
		}

		private IEnumerator FadeInCoroutine()
		{
			while (!hasStarted)
			{
				yield return null;
			}

			while (fader.color.a > 0.01f)
			{
				fader.color = new Color(
					fader.color.r,
					fader.color.g,
					fader.color.b,
					fader.color.a - Time.deltaTime / FadeDuration
				);
				yield return null;
			}
		}

		public void FadeOut()
		{
			StopAllCoroutines();
			StartCoroutine(FadeOutCoroutine());
		}

		private IEnumerator FadeOutCoroutine()
		{
			while (!hasStarted)
			{
				yield return null;
			}

			while (fader.color.a < 0.99f)
			{
				fader.color = new Color(
					fader.color.r,
					fader.color.g,
					fader.color.b,
					fader.color.a + Time.deltaTime / FadeDuration
				);
				yield return null;
			}
		}

		public void HideFader()
		{
			fader.color = Color.clear;
		}

		public void LoadLevel(int index)
		{
			if (index == -1)
			{
				index = SceneManager.GetActiveScene().buildIndex;
			}

			PlayerPrefs.SetInt("bIsLevelAvailable" + index, 1);
			Time.timeScale = 1;
			StopAllCoroutines();
			StartCoroutine(LoadLevelCoroutine(index));
		}

		private IEnumerator LoadLevelCoroutine(int index)
		{
			if (playerController != null)
			{
				playerController.enabled = false;
			}

			while (!hasStarted)
			{
				yield return null;
			}

			yield return FadeOutCoroutine();

			if (index != 0 && index != 1)
			{
				// If the scene to be loaded is NOT the main menu, show the loading screen
				loadingScreen.sprite = loadingSpriteA;
				loadingScreen.enabled = true;
			}

			SceneManager.LoadSceneAsync(index);
			yield return new WaitForSeconds(1);

			if (index != 0 && index != 1)
			{
				loadingScreen.sprite = loadingSpriteB;

				while (Input.GetAxis("Submit") == 0)
				{
					yield return null;
				}

				loadingScreen.enabled = false;
			}

			yield return FadeInCoroutine();

			if (playerController != null)
			{
				playerController.enabled = true;
			}
		}

		public void OnLevelWasLoaded(int index)
		{
			try
			{
				player = FindObjectOfType<Player>();
				playerController = FindObjectOfType<PlayerController>();
				SpawnPoint = GameObject.Find("Level/Spawn Point").transform;
			}
			catch (Exception) { }
		}

		public void PlayerPassOutAndRespawn(Transform spawnPoint)
		{
			StopAllCoroutines();
			StartCoroutine(PlayerPassOutAndRespawnCoroutine(spawnPoint));
		}

		private IEnumerator PlayerPassOutAndRespawnCoroutine(Transform spawnPoint)
		{
			HamsterBall ball = FindObjectOfType<HamsterBall>();

			if (player != null && playerController != null)
			{
				if (ball != null && ball.playerInside)
				{
					ball.enabled = false;
				}
				playerController.enabled = false;

				while (!hasStarted)
				{
					yield return null;
				}

				yield return FadeOutCoroutine();

				if (ball != null && ball.playerInside)
				{
					ball.Ball.transform.position = spawnPoint.position;
					ball.Ball.transform.rotation = spawnPoint.rotation;
				}
				player.transform.position = spawnPoint.position;
				player.transform.rotation = spawnPoint.rotation;

				yield return FadeInCoroutine();

				playerController.enabled = true;
				if (ball != null && ball.playerInside)
				{
					ball.enabled = true;
				}
			}
		}

		public void Quit()
		{
			StopAllCoroutines();
			StartCoroutine(QuitCoroutine());
		}

		private IEnumerator QuitCoroutine()
		{
			while (!hasStarted)
			{
				yield return null;
			}

			yield return FadeOutCoroutine();

			Application.Quit();
		}

		public void ShowTooltip(string resourceName, string axis)
		{
			ShowTooltip(resourceName, axis, false);
		}

		public void ShowTooltip(string resourceName, string axis, bool useCookie)
		{
			if (!useCookie || PlayerPrefs.GetInt("UITooltip" + resourceName, 0) == 0)
			{
				PlayerPrefs.SetInt("UITooltip" + resourceName, 1);
				UITooltip tooltip = new GameObject("UITooltip", typeof(UITooltip)).GetComponent<UITooltip>();

				tooltip.gameObject.transform.SetParent(transform, false);
				tooltip.SetAxis(axis);
				tooltip.SetSprite(Resources.Load<Sprite>("UI/Tooltip" + resourceName));
			}
		}

		public void Start()
		{
			// Canvas setup
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = Int16.MaxValue;
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

			// Transition Fader setup
			fader.color = Color.clear;
			fader.rectTransform.sizeDelta = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
			fader.transform.SetParent(transform, false);

			// Loading Screen setup
			loadingScreen.enabled = false;
			loadingScreen.rectTransform.anchoredPosition = new Vector2(0, 0);
			loadingScreen.rectTransform.anchorMax = new Vector2(0.85f, 0.92f);
			loadingScreen.rectTransform.anchorMin = new Vector2(0.15f, 0.08f);
			loadingScreen.rectTransform.offsetMax = new Vector2(0, 0);
			loadingScreen.rectTransform.offsetMin = new Vector2(0, 0);
			loadingScreen.sprite = loadingSpriteA;
			loadingScreen.transform.SetParent(transform, false);

			hasStarted = true;
		}
	}
}