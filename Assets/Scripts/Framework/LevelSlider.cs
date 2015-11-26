using UnityEngine;

namespace LotsOfTowers.Framework
{
	public class LevelSlider : MonoBehaviour
	{
		public GameObject[] toMove;
		private Vector3[] oldPosition;
		public float increaseBy;
		private bool moveUp = false;

		void Start()
		{
			oldPosition = new Vector3[toMove.Length];
			for (int i = 0; i < toMove.Length; i++)
				oldPosition[i] = toMove[i].transform.position;
		}

		void Update()
		{
			if (moveUp)
				for (int obj = 0; obj < toMove.Length; obj++)
				{
					if (toMove[obj].transform.position.y < oldPosition[obj].y + increaseBy)
						toMove[obj].transform.position = toMove[obj].transform.position + new Vector3(0, increaseBy * Time.deltaTime, 0);
					else
						toMove[obj].transform.position = new Vector3(toMove[obj].transform.position.x, oldPosition[obj].y + increaseBy, toMove[obj].transform.position.z);
				}
			else
				for (int obj = 0; obj < toMove.Length; obj++)
				{
					if (toMove[obj].transform.position.y > oldPosition[obj].y)
						toMove[obj].transform.position = toMove[obj].transform.position + new Vector3(0, -increaseBy * Time.deltaTime, 0);
					else
						toMove[obj].transform.position = oldPosition[obj];
				}
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
				moveUp = true;
			}
		}
		void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player")
			{
				moveUp = false;
			}
		}
	}
}