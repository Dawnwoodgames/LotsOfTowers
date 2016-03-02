using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

    public float shakeY = 0.8f;
    float shakeSpeed = 0.8f;

    void Update()
    {
        Vector2 _newPosition = new Vector2(0, shakeY);
        if (shakeY < 0)
        {
            shakeY *= shakeSpeed;
        }
        shakeY = -shakeY;
        transform.Translate(_newPosition, Space.Self);
    }
}
