using UnityEngine;

namespace LotsOfTowers.Framework
{
	public class LevelSlider : MonoBehaviour
	{
		public GameObject[] toMove;
		private Vector3[] oldPosition;
		public float increaseBy;
        public float speed = 1f;
		private bool moveUp = false;
        private bool isActive = false;

		void Start()
		{
			oldPosition = new Vector3[toMove.Length];
			for (int i = 0; i < toMove.Length; i++)
				oldPosition[i] = toMove[i].transform.position;
		}

		void Update()
		{
            if (!isActive)
                return;
			if (moveUp)
				for (int obj = 0; obj < toMove.Length; obj++)
				{
                    toMove[obj].transform.position = Vector3.MoveTowards(toMove[obj].transform.position, new Vector3(oldPosition[obj].x, oldPosition[obj].y+increaseBy, oldPosition[obj].z), Time.deltaTime * speed);
                    if (toMove[obj].transform.position == new Vector3(oldPosition[obj].x, oldPosition[obj].y+increaseBy, oldPosition[obj].z))
                        isActive = false;
                }
			else
				for (int obj = 0; obj < toMove.Length; obj++)
				{
                    toMove[obj].transform.position = Vector3.MoveTowards(toMove[obj].transform.position, new Vector3(oldPosition[obj].x, oldPosition[obj].y, oldPosition[obj].z), Time.deltaTime * speed);
                    if (toMove[obj].transform.position == new Vector3(oldPosition[obj].x, oldPosition[obj].y, oldPosition[obj].z))
                        isActive = false;
				}
            Debug.Log(isActive);
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
				moveUp = true;
                isActive = true;
			}
		}
		void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player")
			{
				moveUp = false;
                isActive = true;
			}
		}
	}
}