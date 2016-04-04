using UnityEngine;
using System.Collections;
using Nimbi.Actors;

namespace Nimbi.Interaction.Triggers
{
    public class GaryHousePressurePlateTrigger : MonoBehaviour
    {

        public GameObject stairs, mainCamera, cutsceneCamera;
        public GameObject targetPlate, targetStairs;
        public GameObject groundParticle;
        public GameObject levelSlider;

        private bool playerInRange = false;
        private bool plateActivated = false;

        void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        void Update()
        {
            if (playerInRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPlate.transform.position, Time.deltaTime / 4);

                if (transform.position == targetPlate.transform.position)
                {
                    plateActivated = true;
                    levelSlider.transform.localScale = new Vector3(1, 1, 1);
                }
            }

            if (plateActivated)
            {
                stairs.transform.position = Vector3.MoveTowards(stairs.transform.position, targetStairs.transform.position, Time.deltaTime / 2);
                stairs.transform.rotation = Quaternion.Lerp(stairs.transform.rotation, targetStairs.transform.rotation, Time.deltaTime / 2);

                if (stairs.transform.position == targetStairs.transform.position)
                {
                    StartCoroutine(DelayCameraSwitch());

                    plateActivated = false;
                }
            }
        }

        private void OnTriggerStay(Collider coll)
        {
            if (coll.tag == "Player")
            {
                playerInRange = true;
            }

            if (plateActivated)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().DisableMovement();
                cutsceneCamera.SetActive(true);
                mainCamera.SetActive(false);

                groundParticle.GetComponent<ParticleSystem>().Play();

                cutsceneCamera.transform.position = Vector3.MoveTowards(cutsceneCamera.transform.position, GameObject.Find("cutsceneCameraTarget").transform.position, Time.deltaTime / 20);
            }
        }

        IEnumerator DelayCameraSwitch()
        {
            groundParticle.GetComponent<ParticleSystem>().Stop();
            yield return new WaitForSeconds(2);

            mainCamera.SetActive(true);
            cutsceneCamera.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableMovement();
        }
    }
}