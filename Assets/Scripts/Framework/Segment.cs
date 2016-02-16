using UnityEngine;
using System.Collections;

namespace Nimbi.Framework
{
    public class Segment
    {

        public string name;
        public float startTime;
        public int tries;
        public bool finished;

        public Segment():this("") {}
        public Segment(string name) { this.name = name; this.startTime = Time.time; this.tries = 1; }

    }
}