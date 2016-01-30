using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    [RequireComponent(typeof(Renderer))]
    public class MemoryStep : MonoBehaviour
    {
        public MemoryManager manager;
        public int blockNumber = -1;
        public bool Pressed = false;
        private Color baseColor;
        public bool done = false;
        private float height;
        private float startHeight;

        void Start()
        {
            baseColor = GetComponent<Renderer>().material.color;
            startHeight = transform.localPosition.y;
            height = startHeight;
        }

        void OnCollisionEnter(Collision coll)
        {
            if(coll.collider.tag == "Player" && !Pressed && !done)
            {
                Press();
            }
        }

        public void SetNumber(int n,Color c)
        {
            this.blockNumber = n;
            GetComponent<Renderer>().material.color = c;
        }

        private void Press()
        {
            manager.Press(blockNumber);
            Pressed = true;
            if(blockNumber >=0)
                height = startHeight - 0.2f;
            transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
        }

        public void Unpress()
        {
            Pressed = false;
            if(!done)
                height = startHeight;
            transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
        }

        public void Reset()
        {
            blockNumber = -1;
            Pressed = false;
            GetComponent<Renderer>().material.color = baseColor;
            done = false;
            height = startHeight;
            transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
        }
    }
}