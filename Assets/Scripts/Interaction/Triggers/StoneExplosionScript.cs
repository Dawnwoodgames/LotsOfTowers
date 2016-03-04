using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers {
    public class StoneExplosionScript : MonoBehaviour
    {
        public float radius = 5.0f;
        public float power = 10.0f;

   
        public void Explode()
        {
            Vector3 explosionPos = transform.position;
            Rigidbody[] colliders = GetComponentsInChildren<Rigidbody>();

            foreach(Rigidbody rb in colliders)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
            }

        }




    }
}
