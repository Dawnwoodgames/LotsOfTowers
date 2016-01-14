using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class SunshineRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Ray ray;
    private RaycastHit hit;

    private Vector3 inDirection;

    private int nReflections = 4;
    private int nPoints;

    private string input;

    void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        nReflections = Mathf.Clamp(nReflections, 1, nReflections);
        ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * 100, Color.magenta);

        nPoints = nReflections;
        lineRenderer.SetVertexCount(nPoints);
        lineRenderer.SetPosition(0, transform.position);

        for (int i = 0; i <= nReflections; i++)
        {
            if (i == 0)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
                {
                    inDirection = Vector3.Reflect(ray.direction, hit.normal);
                    ray = new Ray(hit.point, inDirection);

                    input = hit.transform.name;
                    Match match = Regex.Match(input, @"Draai_Spiegel_\d+", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                        Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);

                        if (nReflections == 1)
                            lineRenderer.SetVertexCount(++nPoints);

                        lineRenderer.SetPosition(i + 1, hit.point);
                    }
                    else { Debug.Log(input); }
                }
            }
            else
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
                {
                    inDirection = Vector3.Reflect(inDirection, hit.normal);
                    ray = new Ray(hit.point, inDirection);

                    input = hit.transform.name;
                    Match match = Regex.Match(input, @"Draai_Spiegel_\d+", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                        Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);

                        lineRenderer.SetVertexCount(++nPoints);
                        lineRenderer.SetPosition(i + 1, hit.point);
                    }
                    else { Debug.Log(input); }
                }
            }
        }
    }
}