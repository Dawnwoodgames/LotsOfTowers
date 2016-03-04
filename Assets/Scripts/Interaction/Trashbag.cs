using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class Trashbag : MonoBehaviour
    {

        private bool movingUp;
        private bool shot;
        public Gardendoor door;

        public Transform firstspot;

        // Update is called once per frame
        void Update()
        {
            if (movingUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, firstspot.position, 0.2f);
                if (transform.position == Vector3.MoveTowards(transform.position, firstspot.position, 0.2f))
                {
                    shot = true;
                    movingUp = false;
                    door.Open();
                }
            }
            else if (shot)
            {
                transform.position += new Vector3(-1, 0.1f, 0);
            }
        }

        public void MoveUp()
        {
            movingUp = true;
        }

        public void Shoot()
        {
            shot = true;
            movingUp = false;
        }
    }
}