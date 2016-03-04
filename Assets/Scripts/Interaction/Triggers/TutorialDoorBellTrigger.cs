using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class TutorialDoorBellTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                this.transform.parent = coll.transform;

            this.transform.localPosition = new Vector3(0, 1.5f);
            GameObject.Find("Door").GetComponent<TutorialDoorTrigger>().doorBellPickedUp = true;

            Destroy(this);
        }
    }
}