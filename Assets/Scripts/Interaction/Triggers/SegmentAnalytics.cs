using UnityEngine;
using System.Collections;
using Nimbi.Framework;

namespace Nimbi.Interaction.Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    public class SegmentAnalytics : MonoBehaviour
    {

        public string segmentName;
        public bool finish;

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Player" && !finish)
                UnityAnalytics.StartSegment(segmentName);
            else if (collider.tag == "Player" && finish)
                UnityAnalytics.FinishSegment(segmentName);
        }

        void OnDestroy()
        {
            UnityAnalytics.RemoveSegment(name);
        }
    }
}