using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.Actors
{
	public class Player : MonoBehaviour
	{
		// Private fields
		private float charge;
		private Onesie currentOnesie;
		private Onesie defaultOnesie;
		private Onesie[] onesies;

		// Public fields
		public GameObject tooltip;
		public GameObject hudUi;
		public GameObject chargeParticles;

		public GameObject elephantHead;
		public GameObject elephantBody;

        public bool holdingWater;
        public GameObject waterDisplay;

		private GameObject defaultHead;
		private GameObject defaultBody;

		// Properties
		public bool HasFreeSlots
		{
			get { return onesies[0] == null || onesies[1] == null || onesies[2] == null; }
		}

		public float StaticCharge
		{
			get { return charge; }
			set { charge = Mathf.Max(0, Math.Min(value, 100)); }
		}

		public Onesie Onesie
		{
			get { return currentOnesie == null ? defaultOnesie : currentOnesie; }
			set { currentOnesie = onesies.Contains(value) ? value : currentOnesie; }
		}

		public Onesie[] Onesies
		{
			get { return onesies; }
		}

		// Methods
		public Onesie AddOnesie(int index, Onesie onesie)
		{
			if (index > -1 && index < 3 && onesies.Where(o => o.name == onesie.name).Count() == 0)
			{
				Onesie replacedOnesie = onesies.ElementAtOrDefault(index);

				currentOnesie = currentOnesie == replacedOnesie ? onesie : currentOnesie;
				onesies[index] = onesie;

				// HUD - place onesie image to corresponding skill slot
				hudUi.GetComponent<LotsOfTowers.Framework.HeadsUpDisplayScript>().AttachOnesieToSkillSlot(index, onesie.name);
				// Show HUD - skill
				hudUi.GetComponent<LotsOfTowers.Framework.HeadsUpDisplayScript>().skillsUi.SetActive(true);

				return replacedOnesie;
			}

			return null;
		}

		public bool AddOnesieToFirstFreeSlot(Onesie onesie)
		{
			if (onesies[0] == null) {
				onesies[0] = onesie;
				return true;
			}

			if (onesies[1] == null) {
				onesies[1] = onesie;
				return true;
			}

			if (onesies[2] == null) {
				onesies[2] = onesie;
				return true;
			}

			return false;
		}

		public void Awake()
		{
			this.defaultOnesie = Resources.Load("OnesieDefault") as Onesie;
			this.onesies = new Onesie[3];

			defaultHead = GameObject.Find("Head_Default");
			defaultBody = GameObject.Find("Body_Default");
			hudUi = GameObject.Find("HUD");

			// Set up the player
			Physics.gravity = new Vector3(0, -35, 0);
			AddOnesieToFirstFreeSlot(defaultOnesie);
		}

		public void SwitchOnesie(int index)
		{
			if (index > -1 && index < 3 && onesies[index] != null) {
				currentOnesie = onesies[index];

				if (currentOnesie.name == "OnesieElephant" && !elephantHead.activeInHierarchy)
				{
					defaultHead.SetActive(false);
					defaultBody.SetActive(false);

					elephantHead.SetActive(true);
					elephantBody.SetActive(true);
					if (defaultHead.GetComponent<Renderer>().enabled)
					{
						elephantBody.GetComponent<Renderer>().enabled = true;
						elephantHead.GetComponent<Renderer>().enabled = true;
					}
					else
					{
						elephantBody.GetComponent<Renderer>().enabled = false;
						elephantHead.GetComponent<Renderer>().enabled = false;
					}
				}
				else if (currentOnesie.name == "OnesieDefault" && !defaultHead.activeInHierarchy)
				{
					elephantHead.SetActive(false);
					elephantBody.SetActive(false);

					defaultHead.SetActive(true);
					defaultBody.SetActive(true);

					if (elephantHead.GetComponent<Renderer>().enabled)
					{
						defaultHead.GetComponent<Renderer>().enabled = true;
						defaultBody.GetComponent<Renderer>().enabled = true;
					}
					else
					{
						defaultHead.GetComponent<Renderer>().enabled = false;
						defaultBody.GetComponent<Renderer>().enabled = false;
					}
				}
			}
		}

        public void PlayParticles()
        {
            //chargeParticles.GetComponent<ParticleSystem>().Stop();
            chargeParticles.GetComponent<ParticleSystem>().Play();
        }

		public void Update()
		{
            if (holdingWater)
                waterDisplay.SetActive(true);
            else
                waterDisplay.SetActive(false);

			if (StaticCharge > 0)
			{
				if (StaticCharge > 90)
					chargeParticles.GetComponent<ParticleSystem>().loop = true;
				else
					chargeParticles.GetComponent<ParticleSystem>().loop = false;
			}
		}
	}
}