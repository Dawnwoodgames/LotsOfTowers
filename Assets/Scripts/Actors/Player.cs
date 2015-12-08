using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.Actors
{
	public class Player : MonoBehaviour
	{
		// Static fields
		private static Onesie DefaultOnesie;
		private static int MaxOnesies;

		// Private fields
		private float charge;
		private Onesie currentOnesie;
		private Dictionary<int, Onesie> onesies;

		// Public fields
		public GameObject tooltip;
		public GameObject hudUi;
		public GameObject chargeParticles;

		public GameObject elephantHead;
		public GameObject elephantBody;

		private GameObject defaultHead;
		private GameObject defaultBody;

		// Properties
		public bool CanMoveObjects
		{
			get { return Onesie.canMoveObjects; }
		}

		public bool HasFreeSlots
		{
			get { return onesies.Count < MaxOnesies; }
		}

		public int JumpCount
		{
			get { return Onesie.jumpCount; }
		}

		public float JumpPower
		{
			get { return Onesie.jumpPower; }
		}

		public float MovementSpeed
		{
			get { return Onesie.movementSpeed; }
		}

		public bool IsElephant
		{
			get { return Onesie.isElephant; }
		}

		public float StaticCharge
		{
			get { return charge; }
			set { charge = Math.Max(0, Math.Min(value, 100)); }
		}

		public Onesie Onesie
		{
			get { return currentOnesie == null ? DefaultOnesie : currentOnesie; }
			set { currentOnesie = value; }
		}

		public Onesie[] Onesies
		{
			get { return onesies.Values.ToArray(); }
		}

		// Methods
		public Onesie AddOnesie(int index, Onesie onesie)
		{
			if (index > -1 && index < MaxOnesies && onesies.Values.Where(o => o.name == onesie.name).Count() == 0)
			{
				Onesie replacedOnesie = onesies.ElementAtOrDefault(index).Value;

				currentOnesie = currentOnesie == replacedOnesie ? onesie : currentOnesie;
				onesies.Add(index, onesie);

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
			for (int i = 0; i < MaxOnesies; i++)
			{
				if (!onesies.ContainsKey(i))
				{
					AddOnesie(i, onesie);

					return true;
				}
			}

			return false;
		}

		public void Awake()
		{
			Physics.gravity = new Vector3(0, -35, 0);

			DefaultOnesie = Resources.Load("OnesieDefault") as Onesie;
			MaxOnesies = 3;
			onesies = new Dictionary<int, Onesie>(MaxOnesies);

			defaultHead = GameObject.Find("Head_Default");
			defaultBody = GameObject.Find("Body_Default");
		}

		public void Start()
		{
			hudUi = GameObject.Find("HUD");
		}

		public void SwitchOnesie(int index)
		{
			if (onesies.ContainsKey(index))
			{
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

		public void Update()
		{
			if (StaticCharge > 0)
			{
				if (StaticCharge > 70)
					chargeParticles.SetActive(true);
				else
					chargeParticles.SetActive(false);
			}

			if (Input.GetButtonUp("Submit"))
			{
				//Discharge static load
				StaticCharge = 0;
				Debug.Log(chargeParticles.name);
				chargeParticles.SetActive(false);

				//Discharge animation here
			}
		}
	}
}