using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Menu
{
    public class LoadOnClick : MonoBehaviour {

        public GameObject loadingImage;

        public void LoadScene(int level)
        {
            loadingImage.SetActive(true);
            Application.LoadLevel(level); 
        }
	    
        public void Quit()
        {
            Application.Quit();
        }
    }
}