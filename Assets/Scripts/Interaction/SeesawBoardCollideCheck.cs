using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
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
        void OnCollisionExit(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                PlayerOnBoard = false;
            }
        }
    }
}
