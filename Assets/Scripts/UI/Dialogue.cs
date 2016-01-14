using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Nimbi.Framework;

namespace Nimbi.UI
{
    public class Dialogue : MonoBehaviour
    {
        public GameObject dialogueObject;
        public float showForSeconds = 1f;
        public string dialogueText;
        public TypeCollider type = TypeCollider.Player;
        public StartOnTrigger onTrigger = StartOnTrigger.Enter;

        public bool IsActive { get; set; }

        private bool canShow = true;
      
        void Start()
        {
            dialogueObject.GetComponent<Text>().text = "";
            IsActive = false;
        }
        
        void Update()
        {
            canShow = true;
            Dialogue[] dialogues = FindObjectsOfType<Dialogue>();
            foreach(Dialogue d in dialogues)
            {
                if(d.IsActive && d.GetInstanceID() != this.GetInstanceID())
                {
                    canShow = false;
                }
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if(onTrigger == StartOnTrigger.Enter)
            {
                if (canShow)
                {
                    showText(col);
                }
                else
                {
                    StartCoroutine(tryAgainInTwoSeconds(col));
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (onTrigger == StartOnTrigger.Exit)
            {
                if (canShow)
                {
                    showText(col);
                }
                else
                {
                    StartCoroutine(tryAgainInTwoSeconds(col));
                }
            }
        }

        IEnumerator tryAgainInTwoSeconds(Collider col)
        {
            yield return new WaitForSeconds(1f);
            showText(col);
        }

        private void showText(Collider col)
        {
            if (type == TypeCollider.Elephant)
            {
                if (col.name == "Elephant")
                {
                    dialogueObject.GetComponent<Text>().text = dialogueText.Localize();
                    StartCoroutine(hideText());
                    IsActive = true;
                }
            }
            else if (type == TypeCollider.Player)
            {
                if (col.tag == "Player" && !IsActive)
                {
                    dialogueObject.GetComponent<Text>().text = dialogueText.Localize();
                    StartCoroutine(hideText());
                    IsActive = true;
                }
            }
        }

        private void clearText()
        {
            if(dialogueObject.GetComponent<Text>().text == dialogueText.Localize())
            {
                dialogueObject.GetComponent<Text>().text = "";
                Destroy(this);
            }
        } 

        IEnumerator hideText()
        {
            yield return new WaitForSeconds(showForSeconds);
            clearText();
        }
        
    }
}


