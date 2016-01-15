using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class SunshineRenderer : MonoBehaviour
{
    private LineRenderer line;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 inDirection, origin;

    private string input;
    private int nReflections = 8;
    private int nPoints;

    void Awake()
    {
        line = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        nReflections = Mathf.Clamp(nReflections, 0, nReflections);

        origin = transform.position;
        ray = new Ray(origin, transform.forward);

        Debug.DrawRay(origin, transform.forward * 100, Color.magenta);

        nPoints = nReflections;
        line.SetVertexCount(nPoints);
        line.SetPosition(0, origin);

        for (int i = 0; i < nReflections; i++)
        {
            if (i == 0)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
                {
                    if (hit.transform.tag == "Mirror")
                    {
                        inDirection = Vector3.Reflect(ray.direction, hit.normal);
                        ray = new Ray(hit.point, inDirection);

                        Debug.DrawRay(hit.point, hit.normal * 30, Color.blue);

                        if (nReflections == 1)
                        {
                            line.SetVertexCount(++nPoints);
                        }
                    }
                    line.SetPosition(i + 1, hit.point);
                }
            } else
            {
                if (Physics.Raycast(ray.origin,ray.direction,out hit, 100))
                {
                    if (hit.transform.tag == "Mirror")
                    {
                        inDirection = Vector3.Reflect(inDirection, hit.normal);
                        ray = new Ray(hit.point, inDirection);

                        Debug.DrawRay(hit.point, hit.normal * 30, Color.blue);

                        line.SetVertexCount(++nPoints);
                    }
                    line.SetPosition(i + 1, hit.point);
                }
            }
        }
    }
}