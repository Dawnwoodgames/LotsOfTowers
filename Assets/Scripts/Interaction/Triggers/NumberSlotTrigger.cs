using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction.Triggers
{
    public class NumberSlotTrigger : MonoBehaviour
    {
        public GameObject numberslotOne, numberslotTwo, numberslotThree, numberslotFour;
        private bool numberslotSolved, numberslotOneSolved, numberslotTwoSolved, numberslotThreeSolved, numberslotFourSolved;

        // Update is called once per frame
        void Update()
        {
            if (numberslotSolved)
                Debug.Log("winning");
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.name == numberslotOne.name)
                numberslotOneSolved = true;
            if (coll.name == numberslotTwo.name)
                numberslotTwoSolved = true;
            if (coll.name == numberslotThree.name)
                numberslotThreeSolved = true;
            if (coll.name == numberslotFour.name)
                numberslotFourSolved = true;

            if (numberslotOneSolved && numberslotTwoSolved && numberslotThreeSolved && numberslotFourSolved)
                numberslotSolved = true;
        }
    }
}