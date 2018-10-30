using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float moveSpeed;
    public Transform moveToShowRoom_Point;
    public Transform moveToComic_Point;

    private bool moveToShowRoom;
    private bool moveToComic;

    public static CameraControl instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (moveToShowRoom)
        {
            moveToComic = false;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveToShowRoom_Point.position,moveSpeed * Time.deltaTime);
        }
        else if (moveToComic)
        {
            moveToShowRoom = false;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveToComic_Point.position, moveSpeed * Time.deltaTime);
        }
    }

    public void MoveToShowRoom()
    {
        moveToShowRoom = true;
    }

    public void MoveToComic()
    {
        moveToComic = true;
    }

}
