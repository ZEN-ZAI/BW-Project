using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMove : MonoBehaviour {

    public GameObject cursor;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")) && hit.collider.gameObject.GetComponent<Tile>() != null &&
            hit.collider.gameObject.GetComponent<Tile>().pathLevel > 0)
        {
            cursor.SetActive(true);

            cursor.transform.position = (Map.instance.GetBlockPosition(hit.collider.GetComponent<Tile>().col,
                                                              hit.collider.GetComponent<Tile>().row));

        }
        else
        {
            cursor.SetActive(false);
        }
    }
}
