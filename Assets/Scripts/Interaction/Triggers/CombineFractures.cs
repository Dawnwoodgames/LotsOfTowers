﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nimbi.Interaction.Triggers
{
	public class CombineFractures : MonoBehaviour
	{
		public FanSnapTrigger[] fracturesSnapped;
        public GameObject completeFan;
        public GameObject fractures;
		public GameObject outline;

		void Start()
		{
			try
			{
				fracturesSnapped = GetComponentsInChildren<FanSnapTrigger>();
			}
			catch (System.Exception)
			{

				throw;
			}
		}

		// Update is called once per frame
		void Update()
		{
			if(fracturesSnapped.All(f => f.isPlaced == true))
			{
                fractures.SetActive(false);
                completeFan.SetActive(true);
                gameObject.SetActive(false);
				outline.SetActive(false);
			}
		}
	}
}