using UnityEngine;
using UnityEngine.UI;

namespace TestResources.Test2
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Slider zoomSlider;
        private float minFOV = 20f;
        private float maxFOV = 60f;

        void Update()
        {
            float zoomValue = 1 - zoomSlider.value;
            mainCamera.fieldOfView = Mathf.Lerp(minFOV, maxFOV, zoomValue);
        }
    }
}
