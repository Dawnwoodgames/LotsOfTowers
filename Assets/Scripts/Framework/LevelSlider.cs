using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nimbi.Framework
{
    [RequireComponent(typeof(BoxCollider))]
    public class LevelSlider : MonoBehaviour
    {
        public GameObject[] toMove;
        private Vector3[] oldPosition;
        public float increaseBy;
        public float speed = 1f;
        [HideInInspector]
        public bool InTrigger = false;
        private bool[] isActive;

        void Start()
        {
            oldPosition = new Vector3[toMove.Length];
            isActive = new bool[toMove.Length];
            for (int i = 0; i < toMove.Length; i++)
                oldPosition[i] = toMove[i].transform.position;

        }

        void Update()
        {
            if (InTrigger)
            {
                for (int obj = 0; obj < toMove.Length; obj++)
                {
                    if (!isActive[obj])
                        continue;
                    toMove[obj].transform.position = Vector3.MoveTowards(toMove[obj].transform.position, new Vector3(oldPosition[obj].x, oldPosition[obj].y + increaseBy, oldPosition[obj].z), Time.deltaTime * speed);
                    if (toMove[obj].transform.position == new Vector3(oldPosition[obj].x, oldPosition[obj].y + increaseBy, oldPosition[obj].z))
                        isActive[obj] = false;
                }
            }
            else
            {
                for (int obj = 0; obj < toMove.Length; obj++)
                {
                    if (!isActive[obj])
                        continue;
                    toMove[obj].transform.position = Vector3.MoveTowards(toMove[obj].transform.position, new Vector3(oldPosition[obj].x, oldPosition[obj].y, oldPosition[obj].z), Time.deltaTime * speed);
                    if (toMove[obj].transform.position == new Vector3(oldPosition[obj].x, oldPosition[obj].y, oldPosition[obj].z))
                        isActive[obj] = false;
                }
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player" || col.tag == "HamsterBall")
            {
                InTrigger = true;
                for (int i = 0; i < toMove.Length; i++)
                    isActive[i] = true;
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player" || col.tag == "HamsterBall")
            {
                InTrigger = false;
                for (int i = 0; i < toMove.Length; i++)
                    isActive[i] = true;
            }
        }

        void Reset()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
}