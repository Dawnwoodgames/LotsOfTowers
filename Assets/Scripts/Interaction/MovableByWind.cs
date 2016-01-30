using UnityEngine;
using System.Collections;

using Nimbi.Actors;

namespace Nimbi.Interaction
{

    public class MovableByWind : MonoBehaviour
    {

        void OnCollisionStay(Collision col)
        {
            if(col.gameObject.tag == "Player")
            {
                if(col.gameObject.GetComponent<Player>().Onesie.canMoveObjects)
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }

        void OnCollisionExit()
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}

