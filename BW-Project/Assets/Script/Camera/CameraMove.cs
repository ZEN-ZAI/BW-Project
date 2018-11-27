using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public int speed;
    public int speedMoveToPoint;
    public bool moveToPoint;

    public Vector3 targetPosition;
    public Vector3 cameraMoveClamp;
    public float distance;

    public int clipPlane;

    public Vector3 minClamp;
    public Vector3 maxClamp;

    private Vector3 centerPoint;
    public static CameraMove instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GameData.instance.mapSize == 25)
        {
            centerPoint = new Vector3(250, 280, -110);
        }
        else if (GameData.instance.mapSize == 35)
        {
            centerPoint = new Vector3(400, 280, -60);
        }
        else if (GameData.instance.mapSize == 50)
        {
            centerPoint = new Vector3(480, 280, -17);
        }

        Camera.main.transform.position = centerPoint;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.position = centerPoint;
        }

        if (Camera.main.transform.position.y > clipPlane)
        {
            Camera.main.nearClipPlane = 135;
        }
        else
        {
            Camera.main.nearClipPlane = 0;
        }
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minClamp.x, maxClamp.x),
            Mathf.Clamp(transform.position.y, minClamp.y, maxClamp.y),
            Mathf.Clamp(transform.position.z, minClamp.z, maxClamp.z));

        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (moveToPoint)
        {
            Camera.main.transform.position = Vector3.LerpUnclamped(Camera.main.transform.position,
            targetPosition, speedMoveToPoint * Time.deltaTime);

            distance = Vector3.Distance(Camera.main.transform.position, targetPosition);
            if (Vector3.Distance(Camera.main.transform.position, targetPosition) < 5)
            {
                moveToPoint = false;
            }
        }

    }

    public void MoveToPoint(Vector3 position)
    {
        moveToPoint = true;
        targetPosition = Clamp(position);
    }

    private Vector3 Clamp(Vector3 position)
    {
        Vector3 temp = position;
        temp.x += cameraMoveClamp.x;
        temp.y += cameraMoveClamp.y;
        temp.z -= cameraMoveClamp.z;

        return temp;
    }


}
