using System.Collections;
using UnityEngine;

namespace DUI
{
    public class DUI : MonoBehaviour
    {
        private Camera cam;

        public static float cameraHeight;
        public static float cameraWidth;

        public static Vector2 mousePos;

        private void Awake()
        {
            cam = Camera.main;

            //Orthographic setup
            if (cam.orthographic)
            {
                cameraHeight = cam.orthographicSize;
            }
            //Perspective setup
            else
            {
                //make sure vertical fov is 60
                cam.fieldOfView = 60;
                cameraHeight = (transform.position.z - cam.transform.position.z) / Mathf.Sqrt(3);
            }
            cameraWidth = cameraHeight * Screen.width / Screen.height;
        }

        private void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }
}
