using UnityEngine;
using System.Collections;

public class HingedDoor : MonoBehaviour {

    public Vector3 openDegrees;
    public Vector3 closedDegrees;
    public bool closed;
    private Quaternion target;

    void Start()
    {
        if (closed)
            Close();
        else
            Open();

        
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.smoothDeltaTime * 2);
    }

    public void Open()
    {
        target = Quaternion.Euler(openDegrees);
    }

    public void Close()
    {
        target = Quaternion.Euler(closedDegrees);
    }
}
