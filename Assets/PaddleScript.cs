using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction {

public class PaddleScript : MonoBehaviour {


private bool inTrigger;
private Collider paddleCol;

public void Start()
{
    
}

public void Update(){
if(inTrigger && Input.GetButtonDown("Submit"))
{
            this.transform.parent = paddleCol.transform;
            this.transform.localPosition = new Vector3(0, 2.0f);
  }
}

public void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            paddleCol = coll;
            inTrigger = true;
        }
    }
}
