using UnityEngine;
using System.Collections;
using LotsOfTowers.Actors;

namespace LotsOfTowers.Interaction
{
    public class BrokenHamsterwheel : MonoBehaviour
    {
        public float maxDamage = 6.5f;

        private HamsterWheel wheel;
        private Player player;
        private float damage = 0;
        
        private bool broken = false;
        
        void Start()
        {
            wheel = GetComponent<HamsterWheel>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void FixedUpdate()
        {
            if (wheel.GetRotateSpeed() > (maxDamage / 2) && wheel.GetIsPlayerRunning() &&!broken)
            {
                damage += 1 * Time.deltaTime;
                if (damage > (maxDamage / 2))
                {
                    transform.Rotate(new Vector3(.3f, 0));
                    if (damage > (maxDamage / 4))
                    {
                        transform.Rotate(new Vector3(.6f, 0));
                        if (damage > maxDamage)
                        {
                            GameObject.Find("Water").GetComponent<WaterHole>().waterRising = false;
                            transform.Rotate(new Vector3(1f, 0));
                            if (player.Onesie.isElephant)
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