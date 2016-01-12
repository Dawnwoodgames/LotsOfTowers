using UnityEngine;
using Nimbi.Interaction;
using System.Linq;

namespace Nimbi.Environment
{
	public class Aircos : MonoBehaviour
	{
		public GameObject windMirror;

		void Start()
		{
			windMirror.SetActive(false);		
        }
		void Update()
		{
            if (GetComponentsInChildren<PlaceIceBlock>().All(c => c.complete))
			{
				windMirror.SetActive(true);
			}
		}
	}
}