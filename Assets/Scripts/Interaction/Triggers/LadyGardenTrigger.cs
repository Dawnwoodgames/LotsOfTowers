using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using Nimbi.CameraControl;

public class LadyGardenTrigger : MonoBehaviour {

    public AudioClip audioGreeting;

    private GameObject mainCamera;
    private Transform centerFocus;
    private Transform ladyGarden;
    private bool dialoguePlaying = false;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        centerFocus = GameObject.FindGameObjectWithTag("MainCamera").transform.parent;
        ladyGarden = GameObject.Find("LadyGarden").transform;
    }

    void Update()
    {
        if (dialoguePlaying)
        {
            centerFocus.Rotate(Vector3.down * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            mainCamera.GetComponent<MainCameraScript>().degree = -6;
            StartCoroutine(DisableMovement());
            mainCamera.GetComponent<MainCameraScript>().cameraEnabled = false;
            mainCamera.transform.LookAt(ladyGarden);
            dialoguePlaying = true;
            AudioSource.PlayClipAtPoint(audioGreeting, ladyGarden.position);
        }
    }

    IEnumerator DisableMovement()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().DisableMovement();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Animator.SetBool("Moving", false);
        
        yield return new WaitForSeconds(12);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableMovement();
        mainCamera.transform.localRotation = new Quaternion(0, 0, 0, 0);
        mainCamera.GetComponent<MainCameraScript>().cameraEnabled = true;

        Destroy(this);
    }
}
