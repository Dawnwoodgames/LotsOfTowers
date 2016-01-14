using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using UnityEditor;

namespace Nimbi.Interaction
{
	public class CompleteFan : MonoBehaviour
	{
		private Animator animator;
		private bool inTrigger = false;
		private bool isHeavy = false;
		private Player player;
        private int blowCount;
        private bool currentlyDown = false;
        public ParticleSystem fire;

		void Start()
		{
			animator = GetComponentInParent<Animator>();
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		void OnTriggerStay(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = true;
			}
		}

		void OnTriggerExit(Collider coll)
		{
			if (coll.tag == "Player")
			{
				inTrigger = false;
			}
		}

		void Update()
		{
			isHeavy = player.Onesie.isHeavy;

			if (inTrigger && isHeavy)
			{
				animator.SetBool("GoingDown", true);
				animator.SetBool("GoingUp", false);
                if (!currentlyDown && blowCount < 3)
                {
                    blowCount += 1;
                    fire.startSize += 0.2f;
                    SerializedObject so = new SerializedObject(fire);
                    so.FindProperty("ShapeModule.boxX").floatValue += 0.4f;
                    so.ApplyModifiedProperties();
                }
                currentlyDown = true;
                
			}
			else
			{
				animator.SetBool("GoingDown", false);
				animator.SetBool("GoingUp", true);
                currentlyDown = false;
			}
		}
	}
}