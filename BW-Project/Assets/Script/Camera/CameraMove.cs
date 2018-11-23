using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public int speed;
    public int speedMoveToPoint;
    public bool moveToPoint;

    public Vector3 targetPosition;
    public Vector3 clamp;
    public float distance;

    public static CameraMove instance;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveToPoint)
        {
            Camera.main.transform.position = Vector3.LerpUnclamped(Camera.main.transform.position,
            targetPosition, speedMoveToPoint * Time.deltaTime);

            distance = Vector3.Distance(Camera.main.transform.position, targetPosition);
            if (Vector3.Distance(Camera.main.transform.position, targetPosition) < 5)
            {
                moveToPoint = false;
                targetPosition = Vector3.zero;
            }
        }

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

    }

    public void MoveToPoint(Vector3 position)
    {
        moveToPoint = true;
        targetPosition = Clamp(position);
    }

    private Vector3 Clamp(Vector3 position)
    {
        Vector3 temp = position;
        temp.x += clamp.x;
        temp.y += clamp.y;
        temp.z -= clamp.z;

        return temp;
    }


}
