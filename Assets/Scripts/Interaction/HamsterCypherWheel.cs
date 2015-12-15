using UnityEngine;
using LotsOfTowers.Interaction.Triggers;

namespace LotsOfTowers.Interaction
{
	public class HamsterCypherWheel : MonoBehaviour
	{
		public GameObject rotateTrigger;

		private WheelRotateTrigger rotateTriggerScript;
		private float rotateSpeed = 0f;
		private int speedGain = 1;
		private int speedLoss = 3;

		void Start()
		{
			rotateTriggerScript = rotateTrigger.GetComponent<WheelRotateTrigger>();
		}

		void Update()
		{
			if (rotateTriggerScript.GetPlayerRunning())
				if (rotateSpeed < 20)
					rotateSpeed += speedGain;

			if (!rotateTriggerScript.GetPlayerRunning())
				if (rotateSpeed > 0)
					rotateSpeed -= speedLoss * Time.deltaTime;

			gameObject.transform.Rotate(Vector3.up * rotateSpeed);
		}

		public float GetRotateSpeed() { return rotateSpeed; }
	}
}