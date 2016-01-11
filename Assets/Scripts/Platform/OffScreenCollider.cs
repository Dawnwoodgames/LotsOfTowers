using UnityEngine;
using Nimbi.Framework;
using System.Linq;
using System.Collections.Generic;

namespace Nimbi.Platform
{
    public class OffScreenCollider : MonoBehaviour
    {
        public Transform[] resetObjects;
        Dictionary<int, Vector3> startPositions = new Dictionary<int, Vector3>();
        Dictionary<int, Quaternion> startRotations = new Dictionary<int, Quaternion>();

        void Start()
        {
            foreach (Transform go in resetObjects)
            {
                startPositions.Add(go.gameObject.GetInstanceID(), go.transform.position);
                startRotations.Add(go.gameObject.GetInstanceID(), go.transform.rotation);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                try
                {
                    //Player fell off the stage, reset him to the spawn point
                    GameManager.Instance.PlayerPassOutAndRespawn(GameManager.Instance.SpawnPoint);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

            else
            {
                if (resetObjects.Length > 0)
                {
                    Vector3 returnToPos = startPositions.FirstOrDefault(x => x.Key == collision.gameObject.GetInstanceID()).Value;
                    collision.transform.position = new Vector3(returnToPos.x, returnToPos.y + 1f, returnToPos.z);
                    collision.transform.rotation = startRotations.FirstOrDefault(x => x.Key == collision.gameObject.GetInstanceID()).Value;
                }
            }
        }
    }
}
