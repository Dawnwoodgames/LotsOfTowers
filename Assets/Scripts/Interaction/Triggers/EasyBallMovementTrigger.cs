using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class EasyBallMovementTrigger : MonoBehaviour
    {
        public HamsterBall hamsterBall;
        public bool easyOnExit = false;
        public bool removeEasyBallMovement = false;
        
        void OnTriggerEnter(Collider col)
        {
            if(col.tag == "HamsterBall")
            {
                if (removeEasyBallMovement)
                {
                    hamsterBall.EasyBallMovement = false;
                }
                else
                {
                    hamsterBall.EasyBallMovement = !easyOnExit;
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.tag == "HamsterBall")
            {
                if (!removeEasyBallMovement)
                {
                    hamsterBall.EasyBallMovement = easyOnExit;
                }
            }
        }
    }
}
