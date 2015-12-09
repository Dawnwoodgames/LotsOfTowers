using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class WaterHole : MonoBehaviour
    {
        public HamsterWheel wheel;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (wheel.GetRotateSpeed() > 14)
            {
                //transform.localScale += new Vector3(0, 1 * Time.deltaTime, 0);
            }
        }
    }
}