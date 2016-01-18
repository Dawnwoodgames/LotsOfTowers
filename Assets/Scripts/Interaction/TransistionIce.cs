using UnityEngine;
using Nimbi.Interaction.Triggers;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class TransistionIce : MonoBehaviour
	{
		public Transform start;
		public TransistionTrigger startTrigger;
		public Transform end;
		public TransistionTrigger endTrigger;
		public Transform transport;

		private GameObject player;
		private bool insideStartTrigger = false;
		private bool insideEndTrigger = false;
		private TransistionTrigger trigger;
		private int phase = 0;

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		// Update is called once per frame
		void Update()
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

		void FixedUpdate()
		{
			if (insideStartTrigger)
			{
				switch (phase)
				{
					case 0:
						if (transport.localPosition.y >= 1.3f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - Vector3.up, Time.deltaTime * 10);
						}
						else
						{
							phase = 1;
						}
						break;
					case 1:
						if (transport.localPosition.x >= -19.75f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition + Vector3.left, Time.deltaTime * 10);
						}
						else
						{
							phase = 2;
						}
						break;
					case 2:
						if (transport.localPosition.z <= 11)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition + Vector3.forward, Time.deltaTime * 20);
						}
						else
						{
							phase = 3;
						}
						break;
					case 3:
						if (transport.localPosition.x >= -37f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition + Vector3.left, Time.deltaTime * 20);
						}
						else
						{
							phase = 4;
						}
						break;
					case 4:
						if (transport.localPosition.y <= 3f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition + Vector3.up, Time.deltaTime * 10);
						}
						else
						{
							phase = 5;
						}
						break;
					default:
						break;
				}

				if (phase == 5)
				{
					player.transform.parent = null;
					player.transform.localScale = new Vector3(1, 1, 1);
					player.GetComponent<Rigidbody>().isKinematic = false;
					player.GetComponent<PlayerController>().enabled = true;
					insideStartTrigger = false;
					phase = 0;
				}
			}
			else if (insideEndTrigger)
			{
				switch (phase)
				{
					case 0:
						if (transport.localPosition.y >= 1.43f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - Vector3.up, Time.deltaTime * 10);
						}
						else
						{
							phase = 1;
						}
						break;
					case 1:
						if (transport.localPosition.x <= -19.67f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - Vector3.left, Time.deltaTime * 20);
						}
						else
						{
							phase = 2;
						}
						break;
					case 2:
						if (transport.localPosition.z >= -1.47f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - Vector3.forward, Time.deltaTime * 20);
						}
						else
						{
							phase = 3;
						}
						break;
					case 3:
						if (transport.localPosition.x <= -17.5f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition - Vector3.left, Time.deltaTime * 10);
						}
						else
						{
							phase = 4;
						}
						break;
					case 4:
						if (transport.localPosition.y <= 2.75f)
						{
							transport.localPosition = Vector3.MoveTowards(transport.localPosition, transport.localPosition + Vector3.up, Time.deltaTime * 10);
						}
						else
						{
							phase = 5;
						}
						break;
					default:
						break;
				}

				if (phase == 5)
				{
					player.transform.parent = null;
					player.transform.localScale = new Vector3(1, 1, 1);
					player.GetComponent<Rigidbody>().isKinematic = false;
					player.GetComponent<PlayerController>().enabled = true;
					player.transform.localPosition = start.localPosition;
					insideEndTrigger = false;
					phase = 0;
				}
			}
		}
	}
}
