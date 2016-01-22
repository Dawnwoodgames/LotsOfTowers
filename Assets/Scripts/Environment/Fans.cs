using UnityEngine;

namespace Nimbi.Environment
{
	public class Fans : MonoBehaviour
	{
		public Vector3 rotation;
        private Vector3 originalRotation;

        void Start()
        {
            originalRotation = rotation;
        }
		
		void FixedUpdate()
		{
			transform.Rotate(rotation);
		}

        public void Stop()
        {
            rotation = Vector3.zero;
            transform.rotation = Quaternion.Euler(rotation);
        }

        public void Restart()
        {
            rotation = originalRotation;
        }
        
	}
}