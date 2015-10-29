using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace LotsOfTowers.Suits
{
	public class OnesiePickup : MonoBehaviour
	{
		public GameObject tooltip;
		// Use this for initialization
		void OnCollisionEnter(Collision collision)
		{
			//Get gameobject to double the jump power
			ThirdPersonCharacter person = collision.gameObject.GetComponent<ThirdPersonCharacter>();

			person.m_JumpPower = person.m_JumpPower * 2;

			Destroy(gameObject);
			GameObject tip = Instantiate (tooltip) as GameObject;
			tip.GetComponent<ModalPanel> ().Tooltip ("This onesie allows you to jump higher", "Press the jump button to close", KeyCode.Space, false);
        }
	}

}