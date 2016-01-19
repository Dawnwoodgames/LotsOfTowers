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

		if (Input.GetButtonDown ("Submit")) 
		{
			IntroFade ();
		}

		switch (IntroState) {
		    case 1:
			    IntroText.GetComponent<Text>().text = "On a normal sunday morning";
			    GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[0];
			    break;
            case 9:
                GameManager.Instance.LoadLevel(2, true);
                break;
            default:
			    IntroText.GetComponent<Text>().text = ("intro_"+(IntroState)).Localize();
			    GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[IntroState-1];
			    break;
		    
		}
	}
}
}