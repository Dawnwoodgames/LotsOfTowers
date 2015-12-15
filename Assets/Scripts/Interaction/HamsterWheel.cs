using UnityEngine;
using System.Collections;

public class HamsterWheel : MonoBehaviour {

    public GameObject waterToPump;
    public GameObject newWater;
    private float newHeight = 0f;
    private Vector3 defaultPosition;
    private bool pumping;
    public float pumpDelay = 1.0f;
    private float nextPump;
    public GameObject wheel;
    private GameObject player;

    void Start()
    {
        nextPump = Time.time;
        defaultPosition = newWater.transform.localPosition;
    }

    void Update()
    {
        if (nextPump < Time.time && pumping && waterToPump.GetComponent<HamsterWater>().spitcount > 0)
        {
            newHeight += 0.7f;
            nextPump = Time.time + pumpDelay;
            waterToPump.GetComponent<HamsterWater>().spitcount -= 1;
        }
        newWater.transform.localScale = Vector3.MoveTowards(newWater.transform.localScale, new Vector3(newWater.transform.localScale.x, newHeight, newWater.transform.localScale.z), Time.deltaTime * 2);
        newWater.transform.localPosition = Vector3.MoveTowards(newWater.transform.localPosition, new Vector3(newWater.transform.localPosition.x, defaultPosition.y + newHeight, newWater.transform.localPosition.z), Time.deltaTime*2);
        if(pumping)
            wheel.transform.Rotate(new Vector3(0, 0, 5f*(player.transform.position.z-wheel.transform.position.z >0?1:-1)));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            pumping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            pumping = false;
    }
}
