using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using System.Linq;

namespace Nimbi.Interaction
{
	public class FireWood : MonoBehaviour
	{

		public GameObject particle;
		public GameObject water;
		public GameObject cloudToSpawn;
		public float cloudSpeed = 1f;

		GameObject spawnedCloud;
		Vector3 cloudMovePosition = new Vector3(-2.55f, 15.06f, -4.5f);

		private BoilerPressurePlate BoilerLid;
		private Player player;
		private bool hasFireContact;
		private bool isTrigger;
		private bool boilingWater;

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			BoilerLid = GameObject.Find("PressurePlate").GetComponent<BoilerPressurePlate>();
		}

		void Update()
		{
			//Only when fire is hitting the wood + water is not already boiling
			if (hasFireContact && !boilingWater)
			{
				//Activate particle system
				particle.SetActive(true);
				
				//Invoke the repeating cloud spawn system for water clouds
				InvokeRepeating("SpawnCloud", 2 * Time.deltaTime, 5f);

				//Water is now boiling, even when lid is closed
				boilingWater = true;
			}

			//Is there a cloud available?
			if (spawnedCloud != null)
			{
				//Move the cloud if it's not on the end position
				if (spawnedCloud.transform.position != cloudMovePosition)
				{
					//Lerp it!
					spawnedCloud.transform.position = Vector3.MoveTowards(spawnedCloud.transform.position, cloudMovePosition, cloudSpeed * Time.deltaTime);
				}
			}
		}

		void SpawnCloud()
		{
			if (BoilerLid.lidIsOpen)
			{
				spawnedCloud = Instantiate(cloudToSpawn, new Vector3(-5.02f, 18.8f, 5.03f), transform.rotation) as GameObject;
			}
		}

		void OnTriggerEnter(Collider col)
		{
			if (col.tag == "Fire")
			{
				hasFireContact = true;
			}
		}
	}
}