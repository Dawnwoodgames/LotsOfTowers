using UnityEngine;
using Nimbi.Interaction.Triggers;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class Transistion : MonoBehaviour
	{
		public Transform start;
		public TransistionTrigger startTrigger;
		public Transform end;
		public TransistionTrigger endTrigger;
		public Transform transport;

		private GameObject player;
		private bool insideStartTrigger = false;
		private bool insideEndTrigger = false;
		private bool raisingUp = false;
		private TransistionTrigger trigger;

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		// Update is called once per frame
		void Update()
		{
            if(player.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
            {
                if (startTrigger.insideStartTrigger && Input.GetButtonDown("Submit"))
                {
                    player.transform.parent = transport;
                    player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    player.GetComponent<Rigidbody>().isKinematic = true;
                    player.GetComponent<PlayerController>().enabled = false;
                    insideStartTrigger = true;
                }
                else if (endTrigger.insideEndTrigger && Input.GetButtonDown("Submit"))
                {
                    player.transform.parent = transport;
                    player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    player.GetComponent<Rigidbody>().isKinematic = true;
                    player.GetComponent<PlayerController>().enabled = false;
                    insideEndTrigger = true;
                }
            }
		}

		void FixedUpdate()
		{
			if (insideStartTrigger)
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
					player.GetComponent<PlayerController>().enabled = true;
					insideStartTrigger = false;
					raisingUp = false;
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
					transport.localPosition = start.localPosition - (Vector3.up * 20);
					raisingUp = true;
				}
				else if (raisingUp && transport.localPosition != start.localPosition)
				{
					transport.localPosition = Vector3.MoveTowards(transport.localPosition, start.localPosition, Time.deltaTime * 20);
				}
				else
				{
					player.transform.parent = null;
					player.transform.localScale = new Vector3(1, 1, 1);
					player.GetComponent<Rigidbody>().isKinematic = false;
					player.GetComponent<PlayerController>().enabled = true;
					insideEndTrigger = false;
					raisingUp = false;
				}
			}
		}
	}
}
