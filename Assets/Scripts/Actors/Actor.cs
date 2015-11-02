using System;
using System.Collections.Generic;
using LotsOfTowers.Framework;
using LotsOfTowers.ToolTip;
using UnityEngine;

namespace LotsOfTowers.Actors
{
	public class Actor : MonoBehaviour
	{
		// Private fields
		private Onesie onesie;
		private List<Onesie> onesies;

		// Public fields
		public GameObject tooltip;

		// Properties
		public bool CanMoveObjects
		{
			get { return onesie.canMoveObjects; }
		}

		public int JumpCount
		{
			get { return onesie.jumpCount; }
		}

		public float MovementSpeed
		{
			get { return onesie.movementSpeed; }
		}

		public Onesie[] Onesies
		{
			get { return onesies.ToArray(); }
		}

		// Methods
		public void AddOnesie(Onesie onesie)
		{
			if (!onesies.Contains(onesie))
			{
				onesies.Add(onesie);
			}
		}

		public void Awake()
		{
			DontDestroyOnLoad(gameObject);
			this.onesie = Onesie.Load("Default");
			this.onesies = new List<Onesie>(new Onesie[] { onesie });
		}

		public void Equip(Onesie onesie)
		{
			if (onesies.Contains(onesie))
			{
				this.onesie = onesie;
			}
		}

		public void Start()
		{
			Tooltip.ShowTooltip(tooltip, "Movement", false, new string[] { "Horizontal", "Vertical" });
		}
	}
}