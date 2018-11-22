using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float scrollWheel;
    public int zoomPower;

    public int mapZoomOut;
    public int mapZoomIn;

    public float orthographicSizeOrigin;

    public static CameraZoom instance;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0f && Camera.main.orthographicSize != mapZoomIn) // Zoom in
        {
            //transform.Translate(new Vector3(0,0,1) * zoomPower * Time.deltaTime);
            Camera.main.orthographicSize -= zoomPower;
        }
        else if (scrollWheel < 0f && Camera.main.orthographicSize != mapZoomOut) // Zoom out
        {
            //transform.Translate(new Vector3(0, 0, -1) * zoomPower * Time.deltaTime);
            Camera.main.orthographicSize += zoomPower;
        }
    }
}
