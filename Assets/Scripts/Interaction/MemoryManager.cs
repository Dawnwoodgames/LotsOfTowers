using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class MemoryManager : MonoBehaviour
    {
        public MemoryStep[] steps;
        public MemoryStairs[] stairs;
        public Color[] stepcolors;
        public int combinations = 6;

        void Randomize()
        {
            for(int i = 0; i < combinations; i++)
            {
                for (int z = 0; z < 2; z++)
                    PlaceRandomBlock(i);
            }
        }

        public void Reset()
        {
            foreach (MemoryStep s in steps)
                s.Reset();

            foreach (MemoryStairs s in stairs)
                s.Reset();

            Randomize();
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

        public void Press(int number)
        {
            if (number < 0)
                return;

            foreach (MemoryStep s in steps) {
                if(s.Pressed && s.blockNumber == number)
                {
                    bool found = false;
                    for(int i = 0; i < stairs.Length; i++)
                    {
                        if(!found && !stairs[i].IsExpanded)
                        {
                            stairs[i].SetHeight(number);
                            found = true;
                        }
                    }
                    foreach(MemoryStep step in steps)
                    {
                        if(step.blockNumber == number)
                        {
                            step.done = true;
                        }
                    }
                }
                else if (s.Pressed)
                {
                    s.Unpress();
                }
            }
        }
    }
}