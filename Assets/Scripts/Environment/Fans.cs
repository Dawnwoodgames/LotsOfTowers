using UnityEngine;

namespace Nimbi.Environment
{
	public class Fans : MonoBehaviour
	{
		public Vector3 rotation;
		
		// Update is called once per frame
		void Update()
		{
			transform.Rotate(rotation);
		}
	}
}