using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class SeesawBoardCollideCheck : MonoBehaviour {
        
        public bool PlayerOnBoard{
            get; set;
        }

        void OnCollisionStay(Collision col)
        {
            if(col.gameObject.tag == "Player")
            {
                PlayerOnBoard = true;
            }
        }
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                PlayerOnBoard = true;
            }
        }
        void OnCollisionExit(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                PlayerOnBoard = false;
            }
        }
    }
}
