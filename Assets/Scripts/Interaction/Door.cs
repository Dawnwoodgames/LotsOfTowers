using Nimbi.Platform;
using UnityEngine;
using Nimbi.Audio;

namespace Nimbi.Interaction
{
    public class Door : MonoBehaviour
    {
        public GameObject originDoor;

        private GameObject key;
        private bool openingDoor = false;
        private Quaternion originMarker, dupMarker;
        private float speed = 1f;

        public void Start()
        {
            key = GameObject.Find("Key").gameObject;

            dupMarker = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 45, 0);
            originMarker = Quaternion.Euler(0, originDoor.transform.rotation.eulerAngles.y - 45, 0);
        }

        private void Update()
        {
            if (openingDoor)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, dupMarker, Time.deltaTime * speed);
                originDoor.transform.rotation = Quaternion.Lerp(originDoor.transform.rotation, originMarker, Time.deltaTime * speed);
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                if (Input.GetButtonDown("Submit"))
                {
                    if (key != null)
                    {
                        OpenDoor();
                        GameObject.Find("PressurePlate").SetActive(false);
                    }
                }
            }
        }

        private void OpenDoor()
        {
            AudioManager.Instance.PlaySoundeffect(AudioManager.Instance.doorOpenSoundFile);
            Destroy(key);

            openingDoor = true;
        }
    }
}
