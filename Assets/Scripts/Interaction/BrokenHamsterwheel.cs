using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.Interaction.Triggers;

namespace Nimbi.Interaction
{
    public class BrokenHamsterwheel : MonoBehaviour
    {
        public HamsterWheelTrigger rotateTrigger;
        public WaterHole waterHole;
        public float maxDamage = 6.5f;

        private Player player;
        private float damage = 0;
        private bool broken = false;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void FixedUpdate()
        {
            if (rotateTrigger.GetPlayerRunning() && !broken)
            {
                Debug.Log(damage);
                damage += 1 * Time.deltaTime;
                if (damage > (maxDamage / 4))
                {
                    transform.Rotate(new Vector3(0, 0, .05f));
                    if (damage > (maxDamage / 2))
                    {
                        transform.Rotate(new Vector3(0, 0, .15f));
                        if (damage > maxDamage)
                        {
                            waterHole.waterRising = false;
                            transform.Rotate(new Vector3(0, 0, .3f));
                            if (player.Onesie.isHeavy)
                                broken = true;
                        }
                    }
                }
            }
            else if (broken)
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                gameObject.GetComponent<MeshCollider>().convex = true;

                Destroy(transform.parent.GetChild(1).gameObject);
                Destroy(this);
            }
        }
    }
}