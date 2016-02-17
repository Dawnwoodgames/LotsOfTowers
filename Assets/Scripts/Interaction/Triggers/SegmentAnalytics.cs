using UnityEngine;
using System.Collections;
using Nimbi.Framework;

public enum SegmentAnalyticsTriggerType
{
    Start,
    Finish
}

namespace Nimbi.Interaction.Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public class SegmentAnalytics : MonoBehaviour
    {

        public string segmentName;
        public SegmentAnalyticsTriggerType triggertype;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Player" && triggertype == SegmentAnalyticsTriggerType.Start)
                UnityAnalytics.StartSegment(segmentName);
            else if (collider.tag == "Player" && triggertype == SegmentAnalyticsTriggerType.Finish)
                UnityAnalytics.FinishSegment(segmentName);
        }

        void OnDestroy()
        {
            UnityAnalytics.RemoveSegment(name);
        }
    }
}