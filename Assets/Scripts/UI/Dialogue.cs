using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LotsOfTowers.Framework;

namespace LotsOfTowers.UI
{
    public class Dialogue : MonoBehaviour
    {
        public GameObject dialogueObject;
        public float showForSeconds = 1f;
        public string dialogueText;

        public TypeCollider type = TypeCollider.Player;

        private bool triggered = false;

        void Start()
        {
            dialogueObject.GetComponent<Text>().text = "";
        }
        
        void OnTriggerEnter(Collider col)
        {
            if(type == TypeCollider.Elephant)
            {
                if(col.name == "Elephant")
                {
                    dialogueObject.GetComponent<Text>().text = dialogueText.Localize();
                    StartCoroutine(hideText());
                    triggered = true;
                }
            }
            else if(type == TypeCollider.Player)
            {
                if (col.tag == "Player" && !triggered)
                {
                    dialogueObject.GetComponent<Text>().text = dialogueText.Localize();
                    StartCoroutine(hideText());
                    triggered = true;
                }
            }
        }

        private void clearText()
        {
            if(dialogueObject.GetComponent<Text>().text == dialogueText.Localize())
            {
                dialogueObject.GetComponent<Text>().text = "";
            }
        } 

        IEnumerator hideText()
        {
            yield return new WaitForSeconds(showForSeconds);
            clearText();
        }
        
    }
}


