using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Nimbi.Environment
{
    public class SunshineRenderer : MonoBehaviour
    {
        
        public Material lineMat;
        public Mesh cylinderMesh;
        public float rayDiameter;
        public GameObject mirrorWind;
        public GameObject mirrorDoor;
        public GameObject newMirror;
        public GameObject newDoor;
        public Color finishedColor;
        private Material newmat;
        private bool magnifyHit;

        [HideInInspector]
        public bool Locked;

        private List<Vector3> linePositions;
        private Ray ray;
        
        private List<GameObject> lines;

        void Awake()
        {
            lines = new List<GameObject>();
            newmat = new Material(lineMat);
        }

        void Update()
        {
            linePositions = new List<Vector3>();
            
            linePositions.Add(transform.position);
            magnifyHit = false;
            AddRay(transform.position, transform.forward);
            CreateLines(linePositions);
        }

        private void AddRay(Vector3 start, Vector3 direction)
        {
            Ray ray = new Ray(start, direction);
            RaycastHit[] rays = Physics.RaycastAll(ray, 100);
            System.Array.Sort(rays, new RaycastSorter());
            bool mirrorfound = false;
            Debug.DrawRay(start, direction * 100, Color.blue);

            
            foreach (RaycastHit hit in rays)
            {

                if (hit.collider.name == "MagnifyGlass")
                    magnifyHit = true;

                if (mirrorfound || hit.collider.tag == "Player" || hit.collider.name == "MagnifyGlass")
                    continue;


                if (hit.collider.tag == "MirrorDoor" && magnifyHit)
                    Complete();

                mirrorfound = true;
                Debug.DrawRay(hit.point, hit.normal * 4, Color.red);
                linePositions.Add(hit.point);
                if (hit.collider.tag == "Mirror")
                    AddRay(hit.point, Vector3.Reflect(ray.direction, hit.normal).normalized);
            }

            if (!mirrorfound)
                linePositions.Add(ray.origin + ray.direction * 100);
        }

        private void Complete()
        {
            Locked = true;
            newmat.SetColor("_EmissionColor", finishedColor * Mathf.LinearToGammaSpace(4));
            if (mirrorWind.activeInHierarchy)
            {
                mirrorDoor.SetActive(false);
                newMirror.SetActive(true);
                newDoor.SetActive(true);
            }
        }

        private void CreateLines(List<Vector3> positions)
        {
            if(lines.Count < positions.Count-1)
                for(int i = lines.Count; i < positions.Count-1; i++)
                    lines.Add(NewLine());

            if (lines.Count > positions.Count-1)
                for (int i = lines.Count; i > positions.Count-1; i--)
                {
                    Destroy(lines[i-1]);
                    lines.RemoveAt(i - 1);
                }

            for (int i = 0; i < positions.Count - 1; i++)
            {
                GameObject line = lines[i];
                line.transform.position = Vector3.Lerp(positions[i],positions[i+1],0.5f);
                float cylinderDistance = 0.5f * Vector3.Distance(positions[i], positions[i+1]);
                line.transform.localScale = new Vector3(rayDiameter, cylinderDistance, rayDiameter);

                // Make the cylinder look at the main point.
                // Since the cylinder is pointing up(y) and the forward is z, we need to offset by 90 degrees.
                line.transform.LookAt(positions[i+1], Vector3.up);
                line.transform.rotation *= Quaternion.Euler(90, 0, 0);
            }
        }

        private GameObject NewLine()
        {
            GameObject go = new GameObject();
            go.name = "Sunshine Ray";
            MeshFilter ringMesh = go.AddComponent<MeshFilter>();
            ringMesh.mesh = cylinderMesh;

            MeshRenderer ringRenderer = go.AddComponent<MeshRenderer>();
            ringRenderer.material = newmat;

            return go;
        }
    }

    class RaycastSorter : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit a, RaycastHit b)
        {
            return a.distance.CompareTo(b.distance);
        }

    }
}