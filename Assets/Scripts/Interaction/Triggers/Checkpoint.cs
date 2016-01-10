using UnityEngine;
using System.Collections;
using Nimbi;

namespace Nimbi.Interaction.Triggers
{
    public class Checkpoint : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            if(col.tag == "Player")
            {
                GameManager.Instance.SpawnPoint.transform.position = transform.position;
            }
        }

    }
}

