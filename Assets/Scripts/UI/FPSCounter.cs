using Nimbi.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Nimbi.UI
{
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 1.0f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;


        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            UnityAnalytics.StartFPSMeasurement();
        }


        private void Update()
        {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int) (m_FpsAccumulator/fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                UnityAnalytics.AddFPSMeasurement(m_CurrentFps);
            }
        }
    }
}
