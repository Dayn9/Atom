using UnityEngine;

namespace DUI
{
    public class DUI : MonoBehaviour
    {
        /// <summary>
        /// handles the global DUI variables
        /// </summary>

        public static float cameraHeight; //height of the screen in Unity units
        public static float cameraWidth; //width of the screen in Unity Units 

        public static Vector2 mousePos; //position of the mouse in Unity Units

        private void Awake()
        {
            Camera cam = Camera.main;

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
            //only need to calculate mouse position once
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }
}
