using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;

namespace Nimbi.Framework
{
    public enum AnalyticsTypes
    {
        CompleteLevel,
        SegmentDuration
    }

    public static class UnityAnalytics
    {

        private static float totalfps;
        private static int fpsSamples;
        private static bool testmode = true;

        private static List<Segment> segments = new List<Segment>();

        /// <summary>
        /// Sends statistics about completing a level to Unity
        /// </summary>
        /// <param name="level">Name of the level that was completed</param>
        /// <param name="duration">Seconds (!) between the start and end of the level</param>
        public static void CompleteLevel(string level, int duration)
        {
            if(testmode && Debug.isDebugBuild)
                Analytics.CustomEvent("CompleteLevel_"+level, new Dictionary<string, object>{ { "duration", duration }, { "FPS", (totalfps/fpsSamples) }});
            Debug.Log("Average FPS: " + (totalfps / fpsSamples));
        }

        public static void StartFPSMeasurement()
        {
            totalfps = 0;
            fpsSamples = 0;
        }

        public static void AddFPSMeasurement(int fps)
        {
            fpsSamples++;
            totalfps += fps;
        }

        /// <summary>
        /// Starts segment with the specified name
        /// </summary>
        /// <param name="name">Name of the segment to start</param>
        /// <returns>Segment that was started or the segment that is already there.</returns>
        public static Segment StartSegment(string name)
        {
            bool segmentfound = false;
            foreach(Segment s in segments)
            {
                if (s.name == name)
                {
                    segmentfound = true;
                    return s;
                }
            }

            if (!segmentfound)
            {
                Segment seg = new Segment(name);
                segments.Add(seg);
                return seg;
            }
            return new Segment(name);
        }

        /// <summary>
        /// Adds a try to the segment
        /// </summary>
        /// <param name="name">Name of the segment</param>
        public static void AddTries(string name)
        {
            foreach(Segment s in segments)
            {
                if (s.name == name)
                    s.tries++;
            }
        }

        /// <summary>
        /// Finishes a segment and submits the data to Unity Analytics
        /// </summary>
        /// <param name="name">Name of the segment to finish</param>
        public static void FinishSegment(string name)
        {
            foreach(Segment s in segments)
            {
                if(s.name == name && !s.finished)
                {
                    if (testmode && Debug.isDebugBuild)
                        Analytics.CustomEvent("Complete Segment " + s.name, new Dictionary<string, object> { { "duration", (Time.time - s.startTime) }, { "tries", s.tries } });
                    s.finished = true;
                    Debug.Log("Finished " + s.name + " in "+ (Time.time - s.startTime)+"s");
                }
            }
        }

        /// <summary>
        /// Cleans up the segment list whenever you (re)start a level
        /// </summary>
        /// <param name="name">Name of the segment to remove</param>
        public static void RemoveSegment(string name)
        {
            foreach (Segment s in segments)
            {
                if (s.name == name)
                {
                    segments.Remove(s);
                    break;
                }
            }
        }
    }
}