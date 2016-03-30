using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{

    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Animator))]
    public class Grass : MonoBehaviour
    {

        void OnTriggerEnter()
        {
            GetComponent<Animator>().SetTrigger("InTrigger");
        }

        void Reset()
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}