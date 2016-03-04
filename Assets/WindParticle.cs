using UnityEngine;
using System.Collections;

public class WindParticle : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public bool rotating = false;
    public Vector3 direction;

    private float chance;
    private float angle;

    void Start()
    {
        chance = 0;
    }

    void Update()
    {
        if (rotating)
        {
            angle += Time.deltaTime * rotationSpeed;
            if (angle > 360)
            {
                rotating = false;
                angle = 0;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

                transform.localRotation = Quaternion.Euler(angle*-direction.z, angle*direction.y, angle*direction.x);

        }
        transform.Translate(direction * Time.deltaTime * speed); // move forward

        if (!rotating)
        {
            if (Random.Range(0, 10000) < chance)
            {
                rotating = true;
                chance = 0;
            }
            else
                chance += 0.3f;
        }
    }
}