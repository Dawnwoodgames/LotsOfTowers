using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Level
{
    public class LevelTwo : MonoBehaviour
	{
        public void ModifySpawnPoint() {
            GameObject.Find("Spawn Point").transform.position = new Vector3(5.82f, 11.63f, 0);
		}

		public void Start() {
			FindObjectOfType<Player>().AddOnesie(Resources.Load("OnesieElephant") as Onesie);
		}
    }
}