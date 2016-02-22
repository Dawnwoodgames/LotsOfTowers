using UnityEngine;
using Nimbi.Actors;
using System.Collections;

namespace Nimbi.Interaction
{
	public class HamsterWheel : MonoBehaviour
	{
		public GameObject waterToPump;
		public GameObject newWater;
		public float waterHeight = 0.315f;
		private float newHeight = 0f;
		private Vector3 defaultPosition;
		private bool pumping;
		public float pumpDelay = 1.0f;
		private float nextPump;
        private int waterAmount = 0;
		public GameObject wheel;
		private GameObject nut;
        private Vector3 nutgoal;

		void Start()
		{
			nextPump = Time.time;
			defaultPosition = newWater.transform.localPosition;
            nut = GameObject.Find("Nut");
            nutgoal = nut.transform.position;
		}

		void Update()
		{
			if (nextPump < Time.time && pumping && waterToPump.GetComponent<HamsterWater>().spitcount > 0)
			{
				newHeight += waterHeight;
				nextPump = Time.time + pumpDelay;
				waterToPump.GetComponent<HamsterWater>().spitcount -= 1;
                waterAmount++;
                nutgoal += Vector3.up*0.6f;
                
			}

            if(!nut.GetComponent<Nut>().pickedUp)
                nut.transform.position = Vector3.MoveTowards(nut.transform.position, nutgoal, Time.smoothDeltaTime * 1.5f);

            newWater.transform.localScale = Vector3.MoveTowards(newWater.transform.localScale, new Vector3(newWater.transform.localScale.x, newHeight, newWater.transform.localScale.z), Time.deltaTime * 1.5f);
			newWater.transform.localPosition = Vector3.MoveTowards(newWater.transform.localPosition, new Vector3(newWater.transform.localPosition.x, defaultPosition.y + newHeight, newWater.transform.localPosition.z), Time.deltaTime * 1.5f);
			if (pumping)
				wheel.transform.Rotate(new Vector3(5f,0,0));
		}

		void OnTriggerEnter(Collider coll)
		{
			if (coll.tag == "Player" && coll.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
			{
				pumping = true;
			}
		}

		void OnTriggerExit(Collider coll)
		{
			if (coll.tag == "Player")
				pumping = false;
		}
	}
}
