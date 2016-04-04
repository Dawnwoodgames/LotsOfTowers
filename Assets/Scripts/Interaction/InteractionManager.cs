using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour
{

    public Animator interactableObject;
    bool inTrigger;

    void Update()
    {
        if (inTrigger && Input.GetButtonDown("Submit"))
            interactableObject.SetTrigger("Interact");
    }

    void OnTriggerEnter()
    {
        inTrigger = true;
    }

    void OnTriggerExit()
    {
        inTrigger = false;
    }
}
