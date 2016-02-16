using UnityEngine;
using System.Collections;

public class Segment {

    public string name;
    public float startTime;
    public int tries;
    public bool finished;

    public Segment() { this.startTime = Time.time; }
    public Segment(string name) { this.name = name; this.startTime = Time.time; }

}
