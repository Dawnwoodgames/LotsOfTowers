﻿using UnityEngine;
using System.Collections;

namespace Nimbi.Environment
{
    public class MemoryStairs : MonoBehaviour
    {
        public bool IsExpanded;
        private float height;
        private bool moving;
        private float startHeight;
        
        void Start()
        {
            startHeight = transform.position.y;
            height = startHeight;
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, height, transform.position.z), 3*Time.smoothDeltaTime);
        }

        public void SetHeight(int h)
        {
            height = startHeight+0.7f+h*0.36f;
            moving = true;
            IsExpanded = true;
        }

        public void Reset()
        {
            height = startHeight;
        }
    }
}