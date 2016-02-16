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

        private static List<Segment> segments = new List<Segment>();

        /// <summary>
        /// Sends statistics about completing a level to Unity
        /// </summary>
        /// <param name="level">Name of the level that was completed</param>
        /// <param name="duration">Seconds (!) between the start and end of the level</param>
        public static void CompleteLevel(string level, int duration)
        {
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

        public static void StartSegment(string name)
        {
            bool segmentfound = false;
            foreach(Segment s in segments)
            {
                if (s.name == name)
                    segmentfound = true;
            }

            if (!segmentfound)
            {
                segments.Add(new Segment(name));
            }
        }

        public static void AddTries(string name)
        {
            foreach(Segment s in segments)
            {
                if (s.name == name)
                    s.tries++;
            }
        }

        public static void FinishSegment(string name)
        {
            foreach(Segment s in segments)
            {
                if(s.name == name && !s.finished)
                {
                    Analytics.CustomEvent("Complete Segment " + s.name, new Dictionary<string, object> { { "duration", (Time.time - s.startTime) }, { "tries", s.tries } });
                    s.finished = true;
                }
            }
        }
    }
}