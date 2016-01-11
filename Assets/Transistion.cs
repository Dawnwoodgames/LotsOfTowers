using UnityEngine;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
	public class Transistion : MonoBehaviour
	{
		public Transform start;
		public Transform startTrigger;
		public Transform end;
		public Transform endTrigger;
		public Transform transport;

		private GameObject player;
		private bool insideStartTrigger = false;
		private bool insideEndTrigger = false;
		private bool raisingUp = false;

        void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		// Update is called once per frame
		void Update()
		{
			if(TransistionTrigger.insideStartTrigger && Input.GetButtonDown("Submit"))
            {
				player.transform.parent = transport;
				player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
				player.GetComponent<Rigidbody>().isKinematic = true;
				insideStartTrigger = true;
			}
			else if (TransistionTrigger.insideEndTrigger && Input.GetButtonDown("Submit"))
			{
				player.transform.parent = transport;
				player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
				player.GetComponent<Rigidbody>().isKinematic = true;
				insideEndTrigger = true;
			}
		}

		void FixedUpdate()
		{
			if(insideStartTrigger)
			{
				if (!raisingUp && transport.localPosition.y > -25)
				{
					transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - (Vector3.up * 20), Time.deltaTime * 20);
				}
				else if(!raisingUp && transport.localPosition.y < -25)
				{
					transport.localPosition = end.localPosition - (Vector3.up * 20);
					raisingUp = true;
				}
				else if(raisingUp && transport.localPosition != end.localPosition)
				{
					transport.localPosition = Vector3.MoveTowards(transport.localPosition, end.localPosition, Time.deltaTime * 20);
				}
				else
				{
					player.transform.parent = null;
					player.transform.localScale = new Vector3(1,1,1);
					player.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
			else if (insideEndTrigger)
			{
				if (!raisingUp && transport.localPosition.y > -25)
				{
					transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - (Vector3.up * 20), Time.deltaTime * 20);
				}
				else if (!raisingUp && transport.localPosition.y < -25)
				{
					transport.localPosition = end.localPosition - (Vector3.up * 20);
					raisingUp = true;
				}
				else if (raisingUp && transport.localPosition != end.localPosition)
				{
					transport.localPosition = Vector3.MoveTowards(transport.localPosition, end.localPosition, Time.deltaTime * 20);
				}
				else
				{
					player.transform.parent = null;
					player.transform.localScale = new Vector3(1, 1, 1);
					player.GetComponent<Rigidbody>().isKinematic = false;
				}
			}
		}
	}
}
