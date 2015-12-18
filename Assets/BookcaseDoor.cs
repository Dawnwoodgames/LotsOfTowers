using UnityEngine;
using System.Collections;

public class BookcaseDoor : MonoBehaviour {

    private bool playerNear = false;

    void Update()
    {
        if (playerNear)
        {
            gameObject.transform.Rotate(Vector3.up * 20 * Time.deltaTime);
        }
    }

	private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            playerNear = true;
            StartCoroutine(SelfDestroy());
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this);
    }
}
