﻿using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Nimbi.Environment
{
    [RequireComponent(typeof(LineRenderer))]
    public class SunshineRenderer : MonoBehaviour
    {
        private LineRenderer line;
        private Ray ray;

        List<Vector3> linePositions;

        public GameObject mirrorWind;
        public GameObject mirrorDoor;
        public GameObject newMirror;
        public GameObject newDoor;

        void Awake()
        {
            line = this.GetComponent<LineRenderer>();
        }

        void Update()
        {
            linePositions = new List<Vector3>();

            linePositions.Add(transform.position);

            AddRay(transform.position, transform.forward);
            line.SetVertexCount(linePositions.Count);
            line.SetPositions(linePositions.ToArray());
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
                if (mirrorfound || hit.collider.tag == "Player")
                    continue;

                if (hit.collider.tag == "MirrorDoor")
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

        public void Complete()
        {
            // Complete puzzle here once we have a new model
            if (mirrorWind.activeInHierarchy)
            {
                mirrorDoor.SetActive(false);
                newMirror.SetActive(true);
                newDoor.SetActive(true);
            }
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