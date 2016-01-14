using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Nimbi.Interaction{ 
public class IntroSceneScript : MonoBehaviour {


	public GameObject IntroText;
	public int IntroState = 1;
	public Sprite[] introSprites;


	void Awake(){

	}

	// Use this for initialization
	void Start () {
			
	}

	public void IntroFade()
	{
		IntroState++;
	}
			
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp ("e")) 
		{
			IntroFade ();
		}

		switch (IntroState) {
			case 1:
			IntroText.GetComponent<Text>().text = "On a normal sunday morning";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[0];
			break;
			case 2:
			IntroText.GetComponent<Text>().text = "I was playing with a friend";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[1];
			break;
		case 3:
			IntroText.GetComponent<Text>().text = "We embarked on a magical adventure";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[2];
			break;
		case 4:
			IntroText.GetComponent<Text>().text = "When all of a sudden, my friend fell and disapeared between the clouds";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[3];
			break;
		case 5:
			IntroText.GetComponent<Text>().text = "Determined to find him, I dove through the clouds";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[4];
			break;
		case 6:
			IntroText.GetComponent<Text>().text = "But instead of finding my friend, I found something completely different...";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[5];
			break;
		case 7:
			IntroText.GetComponent<Text>().text = "Towers as far as I could see";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[6];
			break;
		case 8:
			IntroText.GetComponent<Text>().text = "Would my friend be (up) there?";
			GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[7];
			break;
			case 9:
			GameManager.Instance.LoadLevel(2, true);
			break;
		}
	}
}
}