using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class JumpTrigger : MonoBehaviour {

        public GameObject libra;

        void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player" && !coll.GetComponent<Player>().Onesie.isHeavy)
            {
                libra.GetComponent<LibraTrigger>().playerReadyToLaunch = true;
            }
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
            {
                libra.GetComponent<LibraTrigger>().playerReadyToLaunch = false;
				coll.GetComponent<Rigidbody>().mass = 1;
				coll.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                GameObject.Find("Level").GetComponent<Level.LevelOne>().ModifySpawnPoint();
			}
		}
    }
}