using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Level
{

    
    public class LevelTwo : MonoBehaviour {

        public Onesie defaultOnesie;
        public Onesie elephantOnesie;
       

        private GameObject player;

	    void Start () {
            player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<Player>().AddOnesieToFirstFreeSlot(defaultOnesie);
            player.GetComponent<Player>().AddOnesie(1, elephantOnesie);

        }


    }


}