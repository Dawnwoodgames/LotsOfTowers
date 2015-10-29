using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace LotsOfTowers.Suits
{
	public class OnesiePickup : MonoBehaviour
	{
		// Use this for initialization
		void OnCollisionEnter(Collision collision)
		{
			//Get gameobject to double the jump power
			ThirdPersonCharacter person = collision.gameObject.GetComponent<ThirdPersonCharacter>();

			person.m_JumpPower = person.m_JumpPower * 2;

			Destroy(gameObject);
        }
	}

}