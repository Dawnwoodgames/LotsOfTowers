using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Level
{
    public class LevelOne : MonoBehaviour
    {
        public Onesie defaultOnesie;

        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<Player>().AddOnesieToFirstFreeSlot(defaultOnesie);
        }
    }

}
