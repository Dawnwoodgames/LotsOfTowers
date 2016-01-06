using UnityEngine;
using System.Collections;

namespace LotsOfTowers.Interaction
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
        private float duration = 1f;
        private float amplitude = 1f;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rend = GetComponent<Renderer>();
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
                HighlightArea();
            else
                rend.material = material;
        }

        private void HighlightArea()
        {
            color = rend.material.color;
            color.b = Mathf.Sin(Time.realtimeSinceStartup) / 2 + .5f;
            color.g = Mathf.Sin(Time.realtimeSinceStartup) / 2 + .5f;
            rend.material.color = color;
        }
    }
}