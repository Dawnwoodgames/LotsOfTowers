using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
	public class HamsterWheel : MonoBehaviour
	{
		public GameObject waterToPump;
		public GameObject newWater;
		private float newHeight = 0f;
		private Vector3 defaultPosition;
		private bool pumping;
		public float pumpDelay = 1.0f;
		private float nextPump;
		public GameObject wheel;
		private GameObject player;
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
				newHeight += 0.63f;
				nextPump = Time.time + pumpDelay;
				waterToPump.GetComponent<HamsterWater>().spitcount -= 1;

                nut.transform.position = new Vector3(nut.transform.position.x, nut.transform.position.y + 1.29f, nut.transform.position.z);
                if (waterToPump.GetComponent<HamsterWater>().spitcount <= 0)
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
				player = other.gameObject;
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
