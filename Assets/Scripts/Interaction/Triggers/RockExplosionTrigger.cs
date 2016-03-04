using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class RockExplosionTrigger : MonoBehaviour
    {

        public float force;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    GetComponent<Rigidbody>().AddExplosionForce(force, hit.point, 5, 0, ForceMode.Impulse);
                }

            }
        }
    }
}

