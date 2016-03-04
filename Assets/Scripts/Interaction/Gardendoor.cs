using UnityEngine;
using System.Collections;

namespace Nimbi.Interaction
{
    public class Gardendoor : MonoBehaviour
    {

        public void Open()
        {
            transform.Rotate(0, 90, 0);
        }
    }
}