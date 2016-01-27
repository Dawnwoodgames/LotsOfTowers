using UnityEngine;
using System.Collections;

namespace Nimbi.Environment
{
    public class MemoryManager : MonoBehaviour
    {
        public MemoryStep[] steps;
        public MemoryStairs[] stairs;
        public Color[] stepcolors;
        public int combinations = 6;

        void Start()
        {
            Randomize();
        }

        void Randomize()
        {
            for(int i = 0; i < combinations; i++)
            {
                for (int z = 0; z < 2; z++)
                    PlaceRandomBlock(i);
            }
        }

        void Reset()
        {

        }

        void PlaceRandomBlock(int i)
        {
            bool placed = false;
            while (!placed)
            {
                int step = Random.Range(0, steps.Length - 1);
                if(steps[step].blockNumber < 0)
                {
                    placed = true;
                    steps[step].SetNumber(i,stepcolors[i]);
                }
            }
        }
    }
}