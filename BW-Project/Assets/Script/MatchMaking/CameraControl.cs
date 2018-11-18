using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float moveSpeed;
    public Transform moveToShowRoom_Point;
    public Transform moveToComic_Point;

    public bool moveToShowRoom;
    public bool moveToComic;

    public static CameraControl instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (moveToShowRoom)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveToShowRoom_Point.position,moveSpeed * Time.deltaTime);
        }

        if (moveToComic)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveToComic_Point.position, moveSpeed * Time.deltaTime);
        }
    }

    public void MoveToShowRoom()
    {
        moveToShowRoom = true;
        moveToComic = false;
    }

    public void MoveToComic()
    {
        moveToComic = true;
        moveToShowRoom = false;
    }

}
