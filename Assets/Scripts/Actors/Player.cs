using System;
using System.Collections.Generic;
using System.Linq;
using LotsOfTowers.ToolTip;
using UnityEngine;

namespace LotsOfTowers.Actors
{
	public class Player : MonoBehaviour
	{
		// Static fields
		private static Onesie DefaultOnesie;
		private static int MaxOnesies;

		// Private fields
		private Onesie currentOnesie;
		private Dictionary<int, Onesie> onesies;
		
		// Public fields
		public GameObject tooltip;
        public GameObject hudUi;
		
		// Properties
		public bool CanMoveObjects
		{
			get { return Onesie.canMoveObjects; }
		}

		public bool HasFreeSlots {
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

		public Onesie Onesie
		{
			get { return currentOnesie == null ? DefaultOnesie : currentOnesie; }
		}
		
		public Onesie[] Onesies
		{
			get { return onesies.Values.ToArray(); }
		}
		
		// Methods
		public Onesie AddOnesie(int index, Onesie onesie)
		{
			if (index > -1 && index < MaxOnesies && onesies.Values.Where(o => o.name == onesie.name).Count() == 0) {
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
				if (!onesies.ContainsKey(i)) {
					AddOnesie(i, onesie);

                    return true;
				}
			}

			return false;
		}

		private void Awake()
		{
			DefaultOnesie = Resources.Load("OnesieDefault") as Onesie;
			MaxOnesies = 3;
			DontDestroyOnLoad(gameObject);
			onesies = new Dictionary<int, Onesie>(MaxOnesies);
		}

		private void Start()
		{
			Tooltip.ShowTooltip(tooltip, "Movement", false, new string[] { "Horizontal", "Vertical" });
            hudUi = GameObject.Find("HUD");
		}

		public void SwitchOnesie(int index)
		{
			if (onesies.ContainsKey(index)) {
				currentOnesie = onesies[index];
			}
		}
	}
}