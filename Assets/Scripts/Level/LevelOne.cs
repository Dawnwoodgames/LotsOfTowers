using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Level
{
    public class LevelOne : MonoBehaviour
    {
		public void ModifySpawnPoint()
		{
			GameObject.Find("Spawn Point").transform.position = new Vector3(-6.7f, 21.49f, 4.08f);
		}
	}

}
