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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            mode.text = "Mode: Create";
            createMode = true;
            rotateMode = false;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
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
                    GameObject tempOld = hit.collider.gameObject;
                    GameObject newBlock = Instantiate(tempObjMouseOverObj, MapManagement.instance.rootMap.transform);
                    newBlock.GetComponent<Tile>().col = tempOld.GetComponent<Tile>().col;
                    newBlock.GetComponent<Tile>().row = tempOld.GetComponent<Tile>().row;

                    newBlock.transform.position = tempOld.transform.position;
                    Destroy(hit.collider.gameObject);
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
                    if (hit.collider.gameObject.transform.localRotation.y >=90)
                    {
                        hit.collider.gameObject.transform.Rotate(Vector3.zero);
                    }
                    hit.collider.gameObject.transform.Rotate(new Vector3(0, hit.collider.gameObject.transform.localRotation.y+90, 0));
                }
            }
        }


    }

    public void select(int num)
    {
        tempObjMouseOverObj = MapManagement.instance.block[num];
        Debug.Log("select ["+num+"]: "+ MapManagement.instance.block[num].name);
    }
}
