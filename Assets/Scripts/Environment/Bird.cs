using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

    public float speed = 5f;
    public float rotationSpeed = 200f;
    public Vector3 direction;
    public Vector3 rotation;

    private float angle;

    void Update()
    {
            angle += Time.deltaTime * rotationSpeed;

            transform.localRotation = Quaternion.Euler(rotation*angle);

        transform.Translate(direction * Time.deltaTime * speed); // move forward
    }
}
