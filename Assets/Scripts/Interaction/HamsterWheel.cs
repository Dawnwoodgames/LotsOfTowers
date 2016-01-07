using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
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

		void Start()
		{
			nextPump = Time.time;
			defaultPosition = newWater.transform.localPosition;
            nut = GameObject.Find("Nut");
		}

		void Update()
		{
			if (nextPump < Time.time && pumping && waterToPump.GetComponent<HamsterWater>().spitcount > 0)
			{
				newHeight += waterHeight;
				nextPump = Time.time + pumpDelay;
				waterToPump.GetComponent<HamsterWater>().spitcount -= 1;
                waterAmount++;

                nut.transform.position = new Vector3(nut.transform.position.x, nut.transform.position.y + 0.645f, nut.transform.position.z);
                if (waterAmount == 4)
                {
                    nut.GetComponent<Rigidbody>().useGravity = true;
                    nut.GetComponent<Rigidbody>().AddForce(new Vector3(-1,0,-.5f) * 4f, ForceMode.Impulse);
                }
			}
			newWater.transform.localScale = Vector3.MoveTowards(newWater.transform.localScale, new Vector3(newWater.transform.localScale.x, newHeight, newWater.transform.localScale.z), Time.deltaTime * 2);
			newWater.transform.localPosition = Vector3.MoveTowards(newWater.transform.localPosition, new Vector3(newWater.transform.localPosition.x, defaultPosition.y + newHeight, newWater.transform.localPosition.z), Time.deltaTime * 2);
			if (pumping)
				wheel.transform.Rotate(new Vector3(5f,0,0));
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
				pumping = true;
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player")
				pumping = false;
		}
	}
}
