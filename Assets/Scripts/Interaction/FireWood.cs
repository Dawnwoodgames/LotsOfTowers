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

        GameObject go;
        Vector3 movePosition = new Vector3(75f + 1, 1.7f, 75f);
        public float speed = 5f;

        public float cloudSpeed = 1f;


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
                boilingWater = true;
                if(BoilerLid.lidIsOpen)
                {
                    InvokeRepeating("SpawnCloud", 0.5f, 5);
                    hasFireContact = false;
                }
            }

			if (go.transform.position != movePosition)
			{
				Vector3 newPos = Vector3.MoveTowards(go.transform.position, movePosition, speed * Time.deltaTime);
				go.transform.position = newPos;
			}
		}

        void SpawnCloud()
        {
            go = Instantiate(cloudToSpawn, new Vector3(-8, 19.95f, -39), transform.rotation)as GameObject;
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