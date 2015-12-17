using UnityEngine;
using System.Collections;

public class FaucetWaves : MonoBehaviour {

    public GameObject waveObject;
    public Transform endMarker;
    private float speed = 3.0f;
    private float startTime;

	void Start () {
        waveObject = Instantiate(waveObject, new Vector3(-2.8f, 8.5f, -2.8f), Quaternion.identity) as GameObject;
	}
	
	void Update () {
        startTime += Time.deltaTime;
        waveObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        waveObject.transform.Translate(-Vector3.left * speed * Time.deltaTime);

        if (startTime >= 1.5f)
        {
            Destroy(waveObject);
            waveObject = Instantiate(waveObject, new Vector3(-2.8f, 8.5f, -2.8f), Quaternion.identity) as GameObject;
            waveObject.name = "Wave";
            startTime = 0;
        }
    }
}
