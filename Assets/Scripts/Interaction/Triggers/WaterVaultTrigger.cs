using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class WaterVaultTrigger : MonoBehaviour
    {
        public GameObject waterfall;
        public GameObject water;

        private Vector3 startMarker;
        private Vector3 endMarker;
        private float speed = 1.0F;
        private float startTime;
        private float journeyLength;
        private bool vaultActive = true;

        void Start()
        {
            startMarker = water.transform.position;
            endMarker = new Vector3(startMarker.x, startMarker.y - 5, startMarker.z);
            journeyLength = Vector3.Distance(startMarker, endMarker);
        }

        void Update()
        {
            if (!vaultActive)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                water.transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
                if (Input.GetButtonDown("Submit"))
                    TurnOffVault();
        }

        private void TurnOffVault()
        {
            startTime = Time.time;
            vaultActive = false;
            Destroy(waterfall);
            Destroy(GameObject.Find("CurrentPuzzle"));
        }
    }
}