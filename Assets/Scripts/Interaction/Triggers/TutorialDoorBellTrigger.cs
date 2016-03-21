using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class TutorialDoorBellTrigger : MonoBehaviour
    {
        public GameObject door;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                this.transform.parent = coll.transform;

            this.transform.localPosition = new Vector3(0, 2.0f);
            door.GetComponent<TutorialDoorTrigger>().doorBellPickedUp = true;

            Destroy(this);
        }
    }
}