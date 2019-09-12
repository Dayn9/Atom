using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUI : MonoBehaviour
{
    private Camera cam;

    public static float cameraHeight;
    public static float cameraWidth;

    private void Awake()
    {
        cam = Camera.main;

        //make sure vertical fov is 60
        cam.fieldOfView = 60;

        cameraHeight = (-cam.transform.position.z) / Mathf.Sqrt(3);
        cameraWidth = cameraHeight * Screen.width / Screen.height;
    }
}
