using UnityEngine;
using UnityEngine.UI;

namespace LotsOfTowers.UI
{
    [RequireComponent(typeof(Slider))]
    public class CameraSlider : MonoBehaviour
    {
        private Slider slider;

        public void Awake()
        {
            this.slider = GetComponent<Slider>();
        }

        public void Start()
        {
            slider.value = PlayerPrefs.GetFloat("CameraSensitivity", 2f);
        }

        public void Update()
        {
            PlayerPrefs.SetFloat("CameraSensitivity", slider.value);
            slider.value = PlayerPrefs.GetFloat("CameraSensitivity", 2f);
        }
    }
}