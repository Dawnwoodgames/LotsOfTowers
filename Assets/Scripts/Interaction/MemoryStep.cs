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

        void Start()
        {
            baseColor = GetComponent<Renderer>().material.color;
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
        }

        public void Unpress()
        {
            Pressed = false;
        }

        public void Reset()
        {
            blockNumber = -1;
            Pressed = false;
            GetComponent<Renderer>().material.color = baseColor;
        }
    }
}