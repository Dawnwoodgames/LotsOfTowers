using UnityEngine;
using System.Collections;
using Nimbi.Framework;

namespace Nimbi.Interaction.Triggers
{
	public class TransistionTrigger : MonoBehaviour
	{
		public TriggerType triggerType;
		public static bool insideStartTrigger = false;
		public static bool insideEndTrigger = false;

		void OnTriggerStay(Collider coll)
		{
			if(coll.tag == "Player")
			{
				switch (triggerType)
				{
					case TriggerType.Start:
						insideStartTrigger = true;
                        break;
					case TriggerType.End:
						insideEndTrigger = false;
                        break;
					default:
						break;
				}
			}
		}

		void OnTriggerExit()
		{
			insideStartTrigger = false;
			insideEndTrigger = false;
		}
	}
}