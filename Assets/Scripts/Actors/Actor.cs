using System.Collections.Generic;
using System.Linq;
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

		public float JumpPower
		{
			get { return onesie.jumpPower; }
		}

		public Onesie Onesie
		{
			get { return onesie; }
		}
		
		public Onesie[] Onesies
		{
			get { return onesies.ToArray(); }
		}
		
		// Methods
		public void AddOnesie(Onesie onesie)
		{
			if (onesies.Where(o => o.name == onesie.name).Count() == 0)
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
		
		public Onesie GetNextOnesie(Onesie current) {
			int index = onesies.IndexOf(current);
			
			if (index != -1 && index != onesies.Count) {
				return onesies.ElementAt(index + 1);
			}
			
			return onesies.First();
		}
		
		public Onesie GetPreviousOnesie(Onesie current) {
			int index = onesies.IndexOf(current);
			
			if (index > 0) {
				return onesies.ElementAt(index - 1);
			}
			
			return onesies.Last();
		}
		
		public void Start()
		{
			Tooltip.ShowTooltip(tooltip, "Movement", false, new string[] { "Horizontal", "Vertical" });
		}
	}
}