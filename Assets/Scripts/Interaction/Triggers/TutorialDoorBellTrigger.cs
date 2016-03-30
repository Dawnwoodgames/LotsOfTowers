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

            this.transform.localPosition = new Vector3(0, 2.0f);
            GameObject.Find("FirstGate").GetComponent<TutorialDoorTrigger>().doorBellPickedUp = true;
            GameObject.Find("BellIndicator").SetActive(true);
            GameObject.Find("BellIndicator").GetComponent<Renderer>().materials = GetComponent<Renderer>().materials;

            Destroy(this);
        }
    }
}