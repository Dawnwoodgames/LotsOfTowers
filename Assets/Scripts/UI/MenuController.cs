using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nimbi.UI
{
	public class MenuController : MonoBehaviour
	{
		private new CameraController camera;
		private GameObject currentMenu;
		private EventSystem eventSystem;
		private Text[] labels;
		private GameObject[] menus;

		public ColorBlock colors = ColorBlock.defaultColorBlock;
		public Font font = /*Resources.GetBuiltinResource<Font>("Arial.ttf")*/null;

		public void Awake()
		{
			this.camera = FindObjectOfType<CameraController>();
			this.eventSystem = FindObjectOfType<EventSystem>();
			this.font = (font == null) ? Resources.GetBuiltinResource<Font>("Arial.ttf") : font;
			this.labels = GetComponentsInChildren<Text>();
			this.menus = GetComponentsInChildren<Canvas>().Select(c => c.gameObject).ToArray();

			GetComponentsInChildren<Selectable>().ToList().ForEach(s => s.colors = colors);

			foreach (Text label in labels)
			{
				label.font = font;
				label.text = label.text.Localize();
			}

			if (eventSystem == null)
			{
				eventSystem = new GameObject("Event System", typeof(EventSystem)).GetComponent<EventSystem>();
			}
		}

		public void SetActiveMenu(GameObject menu)
		{
			if (menus.Contains(menu))
			{
				camera.mount = GameObject.Find(menu.name + "/Mounting Point").transform;
				currentMenu = menu;

				if (eventSystem.currentSelectedGameObject == null || eventSystem.currentSelectedGameObject.transform.parent.gameObject != menu)
				{
					try
					{
						eventSystem.SetSelectedGameObject(menu.GetComponentsInChildren<Selectable>().FirstOrDefault().gameObject);
					}
					catch (NullReferenceException) { }
				}
			}
		}

		public void Start()
		{
			if (camera != null)
			{
				SetActiveMenu(menus.FirstOrDefault());
			}
		}

		public void Update()
		{
			if (currentMenu != null && eventSystem.currentSelectedGameObject == null)
			{
				if (currentMenu.name != "Preferences")
				{
					SetActiveMenu(menus.FirstOrDefault());
				}
			}
		}

		// Event handles used by the menu
		public void ChangeLanguage(string language)
		{
			GameManager.Instance.Language = language;
			GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
		}

		public void LoadLevel(int index)
		{
			GameManager.Instance.LoadLevel(index);
		}

		public void ResetGameData()
		{
            // Destroy sliders because they continuously alter game data
            Destroy(FindObjectOfType<AudioSlider>());
            Destroy(FindObjectOfType<CameraSlider>());
            
            // Destroy all game data (R.I.P.)
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();

            // Reset LanguageManager & reload level
			GameManager.Instance.Language = "en";
			GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex, true);
		}

		public void QuitApplication()
		{
			GameManager.Instance.Quit();
		}
	}
}