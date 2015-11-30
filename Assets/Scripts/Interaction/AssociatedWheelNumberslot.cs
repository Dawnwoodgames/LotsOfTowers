using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
{
    public class AssociatedWheelNumberslot : MonoBehaviour
    {
        public GameObject[] numberSlots;
        public int rotateCount;
        
        private HamsterWheel wheel;
        private bool slotIsSpinning = false;

        void Start()
        {
            wheel = GetComponent<HamsterWheel>();
        }

        void Update()
        {
            if (wheel.GetRotateSpeed() >= 20 && !slotIsSpinning)
                RotateQuarterNumberslot();
        }

        private void RotateQuarterNumberslot()
        {
            foreach (GameObject numberslot in numberSlots)
            {
                slotIsSpinning = true;
                numberslot.transform.Rotate(Vector3.left * (90 * rotateCount));
                StartCoroutine(DelaySpinningSlot());
            }
        }

        private IEnumerator DelaySpinningSlot()
        {
            yield return new WaitForSeconds(1f);
            slotIsSpinning = false;
        }
    }
}