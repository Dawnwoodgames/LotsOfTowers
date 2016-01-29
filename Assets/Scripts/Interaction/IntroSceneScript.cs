using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Nimbi.Interaction
{ 
    public class IntroSceneScript : MonoBehaviour
    {

	    public GameObject IntroText;
	    public int IntroState = 0;
	    public Sprite[] introSprites;

	    void FixedUpdate () {
            
		    switch (IntroState) {
			    case 8:
                    GameManager.Instance.LoadLevel(2);
                    break;
                default:
			        IntroText.GetComponent<Text>().text = ("intro_"+(IntroState+1)).Localize();
			        GameObject.Find("ImageIntroScreen").GetComponent<Image>().sprite = introSprites[IntroState];
			        break;
		    
		    }

		    if (Input.GetButtonDown ("Submit")) 
		    {
		        if (IntroState <8) {
			        IntroState++;
		        }
		    }
	    }
    }
}