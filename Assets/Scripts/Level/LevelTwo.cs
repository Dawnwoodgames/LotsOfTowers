using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Level
{
    public class LevelTwo : MonoBehaviour
    {
        public Onesie defaultOnesie;
        public Onesie elephantOnesie;

        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<Player>().AddOnesieToFirstFreeSlot(defaultOnesie);
            player.GetComponent<Player>().AddOnesie(1, elephantOnesie);
        }

        public void ModifySpawnPoint() {
            GameObject.Find("Spawn Point").transform.position = new Vector3(-1.8f, 5.16f, -2.75f);
        }
    }
}