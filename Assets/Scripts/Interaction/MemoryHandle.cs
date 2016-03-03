using UnityEngine;
using System.Collections;
using Nimbi.CameraControl;

namespace Nimbi.Interaction
{
    public class MemoryHandle : MonoBehaviour
    {

        public GameObject rock;
        public GameObject handle;
        private bool inTrigger;
        public float rockSpeed = 1000f;

        public CameraController camera;


        private Vector3 endRotation;


        private bool isActivated;

        void Start()
        {
            endRotation = new Vector3(314, 0, 0);
        }

        void Update()
        {
            if(inTrigger && Input.GetButtonDown("Submit") && !isActivated)
            {
                endRotation = endRotation + new Vector3(55, 0, 0);
                camera.ChangeCamera();
                isActivated = true;
                if (rock != null)
                    DestroyRock();
                camera.cameraHasSwitched = true;
                Invoke("DeactivateCamera", 3);
                Debug.Log("Hmmz. I waited 3 seconds");
            }
           
            handle.transform.localRotation = Quaternion.Slerp(handle.transform.localRotation, Quaternion.Euler(endRotation), 0.2f);
            

        }

        public void DestroyRock()
        {

            rock.transform.position = Vector3.down * 0.1f * Time.deltaTime;
        }


        public void DeactivateCamera()
        {
            camera.DeactivateCamera();
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = true;
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.tag == "Player")
                inTrigger = false;
        }
    }
}