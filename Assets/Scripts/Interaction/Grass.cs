using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(SphereCollider))]
    public class Grass : MonoBehaviour
    {

        Vector3[] defaultVertices;
        float maximumDeformation = 0.4f;
        Vector3 currentDeformation = new Vector3();
        Vector3 previousDeformation = new Vector3();

        bool inTrigger;

        void Start()
        {
            defaultVertices = GetComponent<MeshFilter>().mesh.vertices;
        }
        void Update()
        {
            if (previousDeformation != currentDeformation)
            {
                Mesh mesh = GetComponent<MeshFilter>().mesh;
                Vector3[] vertices = mesh.vertices;
                Bounds bounds = mesh.bounds;
                int i = 0;
                while (i < vertices.Length)
                {
                    if (inTrigger)
                        vertices[i] = defaultVertices[i] + new Vector3(Mathf.Clamp(currentDeformation.x * 0.1f * (vertices[i].y + bounds.extents.y), -maximumDeformation, maximumDeformation), 0, Mathf.Clamp(currentDeformation.z * 0.1f * (vertices[i].y + bounds.extents.y), -maximumDeformation, maximumDeformation));
                    else
                        vertices[i] = defaultVertices[i];
                    i++;

                }
                mesh.vertices = vertices;
            }
            previousDeformation = currentDeformation;
        }

        void OnTriggerEnter()
        {
            inTrigger = true;
        }

        void OnTriggerExit()
        {
            inTrigger = false;
            currentDeformation = new Vector3();
        }

        void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                Vector3 distance = new Vector3(coll.transform.position.x - transform.position.x, 0, coll.transform.position.z - transform.position.z);

                currentDeformation = distance.normalized * -1f;
                //Debug.Log(currentDeformation.ToString("F4"));

                //Debug.DrawRay(transform.position, currentDeformation * 20, Color.red);
            }
        }

        void Reset()
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}