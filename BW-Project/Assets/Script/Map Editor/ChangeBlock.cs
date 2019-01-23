using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBlock : MonoBehaviour
{
    public GameObject tempObjMouseOverObj;
    public Text mode;

    public bool createMode;
    public bool rotateMode;

    // Start is called before the first frame update
    void Start()
    {
        mode.text = "Mode: Create";
        createMode = true;
        rotateMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            mode.text = "Mode: Create";
            createMode = true;
            rotateMode = false;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            mode.text = "Mode: Rotate";
            createMode = false;
            rotateMode = true;
        }

        if (createMode)
        {
            if (tempObjMouseOverObj != null && Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")) && hit.collider.GetComponent<Tile>() != null)
                {
                    GameObject newBlock = Instantiate(tempObjMouseOverObj, MapEditor.instance.rootMap.transform);

                    newBlock.GetComponent<Tile>().row = hit.collider.gameObject.GetComponent<Tile>().row;
                    newBlock.GetComponent<Tile>().col = hit.collider.gameObject.GetComponent<Tile>().col;
                    newBlock.transform.position = hit.collider.transform.position;
                    newBlock.name = newBlock.name.Replace("(Clone)", "").Trim();

                    Destroy(hit.collider.gameObject);

                    MapEditor.instance.tempNameBlock[newBlock.GetComponent<Tile>().row, newBlock.GetComponent<Tile>().col] = newBlock.name+":"+0;
                }
            }
        }
        else if (rotateMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")) && hit.collider.GetComponent<Tile>() != null)
                {
                    hit.collider.transform.Rotate(new Vector3(0, 90, 0));
                    MapEditor.instance.tempNameBlock[hit.collider.GetComponent<Tile>().row, hit.collider.GetComponent<Tile>().col] =
                        hit.collider.name+":"+ hit.collider.transform.rotation.eulerAngles.y;


                }
            }
        }


    }

    public void select(int num)
    {
        tempObjMouseOverObj = MapEditor.instance.block[num];
        Debug.Log("select [" + num + "]: " + MapEditor.instance.block[num].name);
    }
}
