using UnityEngine;
using System.Collections;

namespace Nimbi.Environment
{
    [RequireComponent(typeof(Renderer))]
    public class MemoryStep : MonoBehaviour
    {
        public MemoryManager manager;
        public int blockNumber = -1;
        public bool Pressed = false;

        void Start()
        {

        }

        void Update()
        {

        }

        void OnCollisionEnter(Collision coll)
        {
            if(coll.collider.tag == "Player")
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
    }
}