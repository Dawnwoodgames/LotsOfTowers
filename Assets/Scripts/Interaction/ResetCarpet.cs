using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    [RequireComponent(typeof(BoxCollider))]
    public class ResetCarpet : MonoBehaviour
    {
        public GameObject carpet;
        
        private Vector3 carpetStartPosition;
        private FloatingCarpet carpetObject;

        private bool resetting = false;
        private float startTime;
        private float journeyLength;
        
        void Start()
        {
            carpetStartPosition = carpet.transform.position;
            carpetObject = carpet.GetComponent<FloatingCarpet>();
        }
        
        void OnTriggerEnter()
        {
            if(carpetObject.isFlightFinished())
            {
                startTime = Time.time;
                journeyLength = Vector3.Distance(carpet.transform.position, carpetStartPosition);
                resetting = true;

                carpetObject.resetCarpet();
            }
        }

        void FixedUpdate()
        {
            if(resetting)
            {
                float distCovered = (Time.time - startTime) * 1f;
                float fracJourney = distCovered / journeyLength;

                carpet.transform.position = Vector3.MoveTowards(carpet.transform.position, carpetStartPosition, fracJourney);

                if (Vector3.Distance(carpet.transform.position, carpetStartPosition) < 0.1f)
                {
                    carpetObject.setState(0);
                    resetting = false;
                }
            }            
        }
    }
}
