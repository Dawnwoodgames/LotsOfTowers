using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class Binocular : MonoBehaviour
	{

		public Image levelPreview;
		bool isDisplayed = false;
		RectTransform imageTransform;
		// Use this for initialization
		void Start()
		{
			levelPreview.gameObject.SetActive(false);
			imageTransform = levelPreview.GetComponent<RectTransform>();
		}

		// Update is called once per frame
		void Update()
		{
            if (isDisplayed && (imageTransform.rect.width < Screen.width || imageTransform.rect.height < Screen.height))
            {
                imageTransform.sizeDelta = new Vector2(imageTransform.rect.width + Screen.width / 2f * Time.deltaTime, imageTransform.rect.height + Screen.width / 2f * Time.deltaTime);
            }

        }

		void OnTriggerEnter(Collider coll)
		{
				isDisplayed = true;
				levelPreview.gameObject.SetActive(true);
				coll.GetComponent<PlayerController>().enabled = false;
		}
		/*void OnTriggerExit(Collider coll)
		{
			if (coll.tag == "Player")
			{
				isDisplayed = false;
				imageTransform.sizeDelta = startSize;
				levelPreview.gameObject.SetActive(false);
			}
		}*/
	}
}
