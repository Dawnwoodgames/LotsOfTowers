using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class InteractableObjectOutline : MonoBehaviour
    {
        public float outlineWidth;
        public Material material;

        private GameObject player;
        private Renderer rend;
        private Material newMaterial;
        private Color color;
        private float distance;

        private Color defaultColor;

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

        void FixedUpdate()
        {
            distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (distance < 3)
            {
                HighlightArea();
            }
            else
            {
                rend.material.color = defaultColor;
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