using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class BrokenHamsterwheel : MonoBehaviour
    {
        private HamsterWheel wheel;
        private Actors.Player player;
        private float damage = 0;
        private bool broken = false;
        
        void Start()
        {
            wheel = GetComponent<HamsterWheel>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actors.Player>();
        }

        void Update()
        {
            if (wheel.GetRotateSpeed() > 4 && !broken)
            {
                damage += 1 * Time.deltaTime;

                if (damage > 3)
                {
                    transform.Rotate(new Vector3(.5f, 0));
                    if (damage > 6)
                    {
                        transform.Rotate(new Vector3(1, 0));
                        if (player.Onesie.isElephant)
                            broken = true;
                    }
                }
            }
            else if (broken)
            {
                gameObject.AddComponent<Rigidbody>();
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}