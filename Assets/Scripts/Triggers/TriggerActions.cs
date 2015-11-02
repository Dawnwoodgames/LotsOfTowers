using UnityEngine;
using LotsOfTowers.ToolTip;

namespace LotsOfTowers.Triggers
{
	public class TriggerActions : MonoBehaviour
	{
		public GameObject tooltip;

		void OnTriggerEnter(Collider other)
		{
			if(other.tag == "Player")
				Tooltip.ShowTooltip(tooltip, "Jump", false, new string[] { "Jump" });
		}
	}
}