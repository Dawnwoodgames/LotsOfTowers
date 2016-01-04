using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class FurnaceDoor : MonoBehaviour
    {
        public bool cypherComplete = false;
        private bool playerInArea = false;
        private int doorOpenAngle = -90;
        private int doorCloseAngle = 0;
        private float smooth = 2;

        void Update()
        {
            if (cypherComplete)
            {
                Quaternion target = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
            }
        }
    }
}