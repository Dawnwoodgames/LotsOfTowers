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

                if (damage > 4)
                {
                    transform.Rotate(new Vector3(.3f, 0));
                    if (damage > 6)
                    {
                        transform.Rotate(new Vector3(.6f, 0));
                        if (damage > 8)
                        {
                            transform.Rotate(new Vector3(1f, 0));
                            GameObject.Find("Water").GetComponent<WaterHole>().waterRising = false;
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
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                gameObject.GetComponent<MeshCollider>().convex = true;
                Destroy(transform.parent.GetChild(1).gameObject);
                StartCoroutine(FreezePositionTemporarily());
            }
        }

        private IEnumerator FreezePositionTemporarily()
        {
            yield return new WaitForSeconds(1);

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Destroy(this);
        }
    }
}