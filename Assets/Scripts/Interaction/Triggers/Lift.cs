using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour
{

    bool inTrigger;
    public float speed = 1;
    public Transform start;
    public Transform end;
    private bool goingup = true;
    private bool goingdown;
    private bool moving;

    void Update()
    {
        if (moving)
        {
            if (transform.position.y < end.position.y && goingup)
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            else if (transform.position.y > start.position.y && goingdown)
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

            if (goingup && transform.position.y >= end.position.y)
            {
                moving = false;
                goingdown = true;
                goingup = false;
            }
            else if (goingdown && transform.position.y <= start.position.y)
            {
                moving = false;
                goingup = true;
                goingdown = false;
            }

            Debug.Log(goingdown);
        }
    }

    void OnTriggerEnter()
    {
        inTrigger = true;
        moving = true;

    }

    void OnTriggerExit()
    {
        inTrigger = false;
        if (moving)
        {
            goingdown = true;
            goingup = false;
        }
    }
}
