using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LotsOfTowers.UI
{
    public class Dialogue : MonoBehaviour
    {
        public GameObject dialogueObject;
        public float showForSeconds = 1f;
        public string dialogueText;

        private bool triggered = false;

        void Start()
        {
            clearText();
        }
        
        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player" && !triggered)
            {
                dialogueObject.GetComponent<Text>().text = dialogueText;
                StartCoroutine(hideText());
                triggered = true;
            }
        }

        private void clearText()
        {
            dialogueObject.GetComponent<Text>().text = "";
        }

        IEnumerator hideText()
        {
            yield return new WaitForSeconds(showForSeconds);
            clearText();
        }
        
    }
}


