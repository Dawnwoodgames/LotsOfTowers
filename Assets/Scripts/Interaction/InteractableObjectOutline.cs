using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class InteractableObjectOutline : MonoBehaviour
    {
        public float outlineWidth;
        public Material material;
        public bool shittyWorldPlacement = false; // There need to be a trigger box if this is set to true

        private GameObject player;
        private Renderer rend;
        private Material newMaterial;
        private Color color;
        private float distance;
        private Color defaultColor;

        private bool playerInObjectInTriggerZone = false;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rend = GetComponent<Renderer>();
            defaultColor = rend.material.color;

            
        }

        void Start()
        {
            newMaterial = Instantiate(material);
            newMaterial.SetFloat("_Outline", outlineWidth);
        }

        void OnTriggerEnter(Collider col)
        {
            if(shittyWorldPlacement)
            {
                playerInObjectInTriggerZone = true;
            }
        }
        void OnTriggerExit(Collider col)
        {
            if (shittyWorldPlacement)
            {
                playerInObjectInTriggerZone = false;
            }
        }

        void FixedUpdate()
        {
            if(shittyWorldPlacement)
            {
                if (playerInObjectInTriggerZone)
                {
                    HighlightArea();
                }
                else
                {
                    rend.material.color = defaultColor;
                }
            }
            else
            {
                distance = Vector3.Distance(transform.localPosition, player.transform.position);
                if (distance < 3)
                {
                    HighlightArea();
                }
                else
                {
                    rend.material.color = defaultColor;
                }
            }
        }

        private void HighlightArea()
        {
            color = rend.material.color;
            color.b = Mathf.Sin(Time.realtimeSinceStartup * 2) / 2 + .5f;
            color.g = Mathf.Sin(Time.realtimeSinceStartup * 2) / 2 + .5f;
            rend.material.color = color;
        }
    }
}