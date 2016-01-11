using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction
{
    public class LiftingObject : MonoBehaviour
    {
        private bool pickedUp;
        private bool inTrigger = false;
        private Transform player;
        private float smoothLerp = 5;
        private Rigidbody rigid;
        private MeshCollider meshColl;

        private bool onPickupObject = false;

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

        private void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.tag == "Player")
            {
                onPickupObject = true;
            }
        }
        private void OnCollisionExit(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                onPickupObject = false;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player")
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

        void Update()
        {
            if (inTrigger && !onPickupObject)
            {
                if (Input.GetButton("Submit") && player.GetComponent<Player>().Onesie.type == OnesieType.Elephant)
                {
                    pickedUp = true;
                }
            }

            //Move the object with the player if its picked up
            if (pickedUp)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up * 2.5f, Time.deltaTime * smoothLerp);

                if (!rigid.isKinematic)
                {
                    rigid.isKinematic = true;
                    meshColl.isTrigger = true;
                }

                if (!Input.GetButton("Submit") || player.GetComponent<Player>().Onesie.type != OnesieType.Elephant)
                {
                    pickedUp = false;
                    rigid.isKinematic = false;
                    meshColl.isTrigger = false;
                }
            }
        }
    }
}