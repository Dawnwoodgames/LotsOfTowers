using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeFromBlack : MonoBehaviour {

    Image img;
    float levelstart;

    void Awake()
    {
        img = GetComponent<Image>();
        img.color = Color.black;
        levelstart = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        Color c = img.color;
        c.a -= ((Time.time-levelstart)*0.005f);
        img.color = c;
        if (c.a < 0)
            Destroy(gameObject);
	}
}
