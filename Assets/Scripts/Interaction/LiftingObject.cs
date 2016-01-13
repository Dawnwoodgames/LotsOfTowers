using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class LiftingObject : MonoBehaviour
    {
        private bool inTrigger = false;
        private Transform player;
        private float smoothLerp = 5;
        private Rigidbody rigid;
        private MeshCollider meshColl;
        private bool onPickupObject = false;

        public bool pickedUp { get; set; }

        void Start()
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                rigid = GetComponent<Rigidbody>();
                meshColl = GetComponent<MeshCollider>();
            }
            catch (System.Exception ex)
            {
                Nimbi.Framework.Logger.Log(ex);
                throw;
            }
        }

        private void OnTriggerStay(Collider col)
        {
            if (col.tag == "Player" && Input.GetButton("Submit"))
            {
                inTrigger = true;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
            {
                inTrigger = false;
            }
        }

        void FixedUpdate()
        {
            if (inTrigger)
            {
                if (player.GetComponent<Player>().Onesie.type == OnesieType.Elephant)
                {
                    pickedUp = true;
                }
            }
            
            //Move the object with the player if its picked up
            if (pickedUp)
            {
                bool canPickup = true;
                LiftingObject[] lobj = FindObjectsOfType<LiftingObject>();
                foreach(LiftingObject l in lobj)
                {
                    if(l.pickedUp && l.GetInstanceID() != this.GetInstanceID())
                    {
                        canPickup = false;
                    }
                }
                
                if (canPickup)
                {
					GetComponent<MeshCollider>().enabled = false;
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position + Vector3.up * 2.5f, Time.deltaTime * smoothLerp);
                    if (!rigid.isKinematic)
                    {
                        rigid.isKinematic = true;
                        meshColl.isTrigger = true;
                    }
                }

                if (!canPickup || Input.GetButton("Submit") || player.GetComponent<Player>().Onesie.type != OnesieType.Elephant)
                {
					GetComponent<MeshCollider>().enabled = true;
					meshColl.isTrigger = false;
                    rigid.isKinematic = false;
                    pickedUp = false;
                }
            }
        }
    }
}