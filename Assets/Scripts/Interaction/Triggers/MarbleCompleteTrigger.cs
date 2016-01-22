using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction.Triggers
{
    public class MarbleCompleteTrigger : MonoBehaviour
    {
        private bool puzzleCompleted = false;

        private void OnTriggerEnter(Collider coll)
        {
            if (coll.name == "Marble")
                Complete();
        }

        private void Complete()
        {
            puzzleCompleted = true;
        }

        public bool GetPuzzleCompleted() { return puzzleCompleted; }
    }
}