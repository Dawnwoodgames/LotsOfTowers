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
        public GameObject[] boat;
        private bool moveBoat;

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

            if (moveBoat)
            {
                foreach (GameObject g in boat)
                    g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z + Time.deltaTime * 3);

                player.transform.parent = transform.parent.transform.parent.transform.parent;
            }

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
                if (blowCount >= 3)
                    moveBoat = true;
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