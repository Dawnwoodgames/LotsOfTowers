using UnityEngine;
using System.Collections;
using Nimbi.Actors;
using System.Linq;

namespace Nimbi.Interaction
{
    public class FireWood : MonoBehaviour
    {

        public GameObject particle;
        public GameObject water;
        public GameObject cloudToSpawn;
        public float cloudSpeed = 1f;

        GameObject spawnedCloud;
        Vector3 cloudMovePosition = new Vector3(-2.74f, 16.99f, -36.60f);

        private BoilerPressurePlate BoilerLid;
        private Player player;
        private bool hasFireContact;
        private bool isTrigger;
        private bool boilingWater;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            BoilerLid = GameObject.Find("PressurePlate").GetComponent<BoilerPressurePlate>();

            hasFireContact = false;
            isTrigger = false;
            boilingWater = false;

        }

        // Update is called once per frame
        void Update()
        {
            if (hasFireContact == true)
            {
                particle.SetActive(true);
                if (BoilerLid.lidIsOpen)
                {
                    InvokeRepeating("SpawnCloud", 2 * Time.deltaTime, 5f);
                    hasFireContact = false;
                }
                
            }


            if (boilingWater == true && spawnedCloud.transform.position != cloudMovePosition)
            {
                Vector3 newPos = Vector3.MoveTowards(spawnedCloud.transform.position, cloudMovePosition, cloudSpeed * Time.deltaTime);
                spawnedCloud.transform.position = newPos;
            }
        }

        void SpawnCloud()
        {
            boilingWater = true;
            spawnedCloud = Instantiate(cloudToSpawn, new Vector3(-8, 19.95f, -39), transform.rotation) as GameObject;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Fire")
            {
                hasFireContact = true;

            }
        }
    }
}