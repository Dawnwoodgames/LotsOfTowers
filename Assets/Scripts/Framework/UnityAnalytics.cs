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
        /// <summary>
        /// Sends statistics about completing a level to Unity
        /// </summary>
        /// <param name="level">Name of the level that was completed</param>
        /// <param name="duration">Seconds (!) between the start and end of the level</param>
        public static void CompleteLevel(string level, int duration)
        {
            Analytics.CustomEvent("CompleteLevel_"+level, new Dictionary<string, object>{{ "duration", duration }});
        }
    }
}