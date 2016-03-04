using UnityEngine;
using Nimbi.Interaction.Triggers;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
	public class Transition : MonoBehaviour
	{
		public TransistionTrigger startTrigger, endTrigger;
		public Transform start, end, transport, startFinish, endFinish, startGoal, endGoal;

        private bool moving;

        private GameObject player;
		private bool insideStartTrigger;
		private bool insideEndTrigger;
		private bool goingDown;

		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}

		// Update is called once per frame
		void Update()
		{
            if(player.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
            {
                if (startTrigger.insideStartTrigger && Input.GetButtonDown("Submit")
				&& player.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
                {
                    player.transform.parent = transport;
                    player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    player.GetComponent<Rigidbody>().isKinematic = true;
                    player.GetComponent<PlayerController>().enabled = false;
                    insideStartTrigger = true;
                    goingDown = true;
                    moving = true;
                }
                else if (endTrigger.insideEndTrigger && Input.GetButtonDown("Submit")
				&& player.GetComponent<Player>().Onesie.type == OnesieType.Hamster)
                {
                    player.transform.parent = transport;
                    player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    player.GetComponent<Rigidbody>().isKinematic = true;
                    player.GetComponent<PlayerController>().enabled = false;
                    insideEndTrigger = true;
                    goingDown = true;
                    moving = true;
                }
            }
		}

		void FixedUpdate()
		{
            if (moving)
            {
                Vector3 firstgoal = new Vector3(), secondgoal = new Vector3(), secondstart = new Vector3();
                if (insideStartTrigger)
                {

                    firstgoal = startGoal.position;
                    secondgoal = end.position;
                    secondstart = endGoal.position;
                }
                else if (insideEndTrigger)
                {
                    firstgoal = endGoal.position;
                    secondgoal = start.position;
                    secondstart = startGoal.position;
                }

                if (goingDown)
                {
                    transport.position = Vector3.MoveTowards(transport.position, firstgoal, Time.smoothDeltaTime * 15);
                    if (transport.position == Vector3.MoveTowards(transport.position, firstgoal, Time.smoothDeltaTime * 15))
                    {
                        transport.position = secondstart;
                        goingDown = false;
                    }
                }
                else if (!goingDown)
                {
                    transport.position = Vector3.MoveTowards(transport.position, secondgoal, Time.smoothDeltaTime * 15);
                    if (transport.position == Vector3.MoveTowards(transport.position, secondgoal, Time.smoothDeltaTime * 15))
                    {
                        transport.position = Vector3.MoveTowards(transport.position, secondgoal, Time.smoothDeltaTime * 15);
                        moving = false;
                        player.transform.parent = null;
                        player.transform.localScale = new Vector3(1, 1, 1);
                        player.GetComponent<Rigidbody>().isKinematic = false;
                        player.GetComponent<PlayerController>().enabled = true;
                        player.transform.position = insideStartTrigger ? endFinish.position : startFinish.position;
                        insideStartTrigger = false;
                        insideEndTrigger = false;
                    }
                }
            }
			
		}
	}
}
