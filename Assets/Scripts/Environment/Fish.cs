using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

    public Transform goal;

    void Start()
    {
        Debug.Log(Quaternion.LookRotation(goal.position - transform.position).eulerAngles);
    }

    void Update()
    {
        var q = Quaternion.LookRotation(goal.position - transform.position);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, q, 20 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime*0.2f;
    }
    float AngleDir(Vector3 fwd, Vector3 targetDir)
    {
        float angle = Vector3.Angle(fwd, targetDir);
        Vector3 cross = Vector3.Cross(fwd, targetDir);
        if (cross.y < 0) angle = -angle;

        return angle;
    }
}