using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour {

    public float moveSpeed;
    
    public RectTransform comic;
    public RectTransform showRoom;

    public bool moveRight;
    public bool moveLeft;

    public static PanelControl instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (moveLeft)
        {
            comic.localPosition = Vector3.Lerp(comic.localPosition, new Vector3(-2000,0),moveSpeed * Time.deltaTime);
            showRoom.localPosition = Vector3.Lerp(showRoom.localPosition, new Vector3(0, 0), moveSpeed * Time.deltaTime);
        }

        if (moveRight)
        {
            comic.localPosition = Vector3.Lerp(comic.localPosition, new Vector3(0, 0), moveSpeed * Time.deltaTime);
            showRoom.localPosition = Vector3.Lerp(showRoom.localPosition, new Vector3(2000, 0), moveSpeed * Time.deltaTime);
        }
    }

    public void MoveLeft()
    {
        moveLeft = true;
        moveRight = false;
    }

    public void MoveRight()
    {
        moveRight = true;
        moveLeft = false;
    }

}
