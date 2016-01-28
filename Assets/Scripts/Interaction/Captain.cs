using UnityEngine;
using Nimbi.Actors;
using System.Collections;
using Nimbi.UI;

namespace Nimbi.Interaction
{
    public class Captain : MonoBehaviour
    {
        public Onesie hamsterOnesie;

        public GameObject lostNutDialog;
        public GameObject landlobberDialog;
        public GameObject finishNutDialog;
        public GameObject fixBoatDialog;
		public GameObject pressmeDialog;
		public Transform endMarker;

		private Player player;
        private GameObject nut;
        private bool nutDelivered = false;
        private float startTime;
        private float journeyLength;
        private bool firstInteraction = true;
		private bool dialogIsGone = false;
		private OnesieInfoPopup popup;

		void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            nut = GameObject.Find("Nut");
			popup = GameObject.Find("CenterFocus").GetComponent<OnesieInfoPopup>();
		}

		void Update()
        {
            if (nutDelivered)
            {
				GetComponent<Animator>().SetBool("walking", true);
                transform.position = Vector3.MoveTowards(transform.position, endMarker.position, Time.deltaTime * 1.5f);
            }
            if (transform.position == endMarker.position)
            {
				GetComponent<Animator>().SetBool("walking", false);
				Destroy(this);
            }
			if(!popup.IsPopupShowing(OnesieType.Hamster) && !firstInteraction)
			{
				lostNutDialog.SetActive(true);
				landlobberDialog.SetActive(true);
			}
        }

        private void OnTriggerStay(Collider coll)
        {
            /*
            First interaction with the Captain
            Gives Hamster Onesie
            */
            if (coll.tag == "Player" && Input.GetButtonDown("Submit") && firstInteraction)
            {
				pressmeDialog.GetComponent<Dialogue>().clearText();
                popup.ShowPopup(OnesieType.Hamster, 1);
                player.GetComponent<Player>().AddOnesie(hamsterOnesie);
                firstInteraction = false;
            }

            /*
            Second interaction with the Captain
            If nut is equipped, Captain will move aside
            */
            if (coll.tag == "Player" && Input.GetButtonDown("Submit") && !firstInteraction)
            {
                if (nut != null && nut.GetComponent<Nut>().pickedUp)
                {
                    nutDelivered = true;
                    Destroy(nut);
                    finishNutDialog.SetActive(true);
                    fixBoatDialog.SetActive(true);

                }
            }
        }
    }
}