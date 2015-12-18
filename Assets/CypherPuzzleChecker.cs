using UnityEngine;
using System.Collections;

public class CypherPuzzleChecker : MonoBehaviour
{
    public GameObject oven;

    private bool slotOneComplete, slotTwoComplete, slotThreeComplete = false;

    void Update()
    {
        if (slotOneComplete && slotTwoComplete && slotThreeComplete)
            oven.GetComponent<ParticleSystem>().Play();
        else
            oven.GetComponent<ParticleSystem>().Stop();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "slot_1_complete")
            slotOneComplete = true;

        if (coll.name == "slot_2_complete")
            slotTwoComplete = true;

        if (coll.name == "slot_3_complete")
            slotThreeComplete = true;
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.name == "slot_1_complete")
            slotOneComplete = false;

        if (coll.name == "slot_2_complete")
            slotTwoComplete = false;

        if (coll.name == "slot_3_complete")
            slotThreeComplete = false;
    }
}
