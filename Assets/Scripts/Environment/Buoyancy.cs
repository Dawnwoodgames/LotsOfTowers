using Nimbi.Actors;
using UnityEngine;

namespace Nimbi.Environment
{
    public sealed class Buoyancy : MonoBehaviour
    {
        private static readonly int STATE_IDLE = 0;
        private static readonly int STATE_GOING_DOWN = 1;
        private static readonly int STATE_GOING_UP = 2;

        private new GameObject collider;
        private Player player;
        private int state;

        public void Awake()
        {
            this.collider = GetComponentInChildren<Collider>().gameObject;
            this.player = FindObjectOfType<Player>();
        }

        public void Update()
        {
            state = player.Onesie.isHeavy ? (collider.transform.localScale.y > 0 ? STATE_GOING_DOWN : STATE_IDLE) :
                (collider.transform.localScale.y < 0.5 ? STATE_GOING_UP : STATE_IDLE);

            if (state == STATE_GOING_DOWN) {
                collider.transform.localPosition = new Vector3(
                    collider.transform.localPosition.x,
                    Mathf.Max(-0.3f, collider.transform.localPosition.y - 0.15f * Time.deltaTime),
                    collider.transform.localPosition.z
                );
                collider.transform.localScale = new Vector3(
                    collider.transform.localScale.x,
                    Mathf.Max(0.0000000000001f, collider.transform.localScale.y - 0.5f * Time.deltaTime),
                    collider.transform.localScale.z
                );
            } else if (state == STATE_GOING_UP) {
                collider.transform.localPosition = new Vector3(
                    collider.transform.localPosition.x,
                    Mathf.Min(-0.15f, collider.transform.localPosition.y + 0.15f * Time.deltaTime),
                    collider.transform.localPosition.z
                );
                collider.transform.localScale = new Vector3(
                    collider.transform.localScale.x,
                    Mathf.Min(0.5f, collider.transform.localScale.y + 0.5f * Time.deltaTime),
                    collider.transform.localScale.z
                );
            }
        }
    }
}