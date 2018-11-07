using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseScript : MonoBehaviour
{
    public Material aMouseOverMaterial;
    public Material aMouseSelectMaterial;

    public GameObject tempObjSelectCharacter;
    private GameObject tempObjMouseOverObj;

    public void SelectToMove(ref bool select,ref int enegy, PathFinder pathFinder)
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")) && hit.collider.GetComponent<Tile>() != null
                && hit.collider.GetComponent<Tile>().pathLevel > 0)
            {

                int tempChar_x = tempObjSelectCharacter.GetComponent<Character>().x; int target_x = hit.collider.GetComponent<Tile>().col;
                int tempChar_y = tempObjSelectCharacter.GetComponent<Character>().y; int target_y = hit.collider.GetComponent<Tile>().row;

                tempObjSelectCharacter.GetComponent<Character>().WalkToBlock(hit.collider.GetComponent<Tile>().col, hit.collider.GetComponent<Tile>().row);
                tempObjSelectCharacter.GetComponent<SetMaterial>().SetDefaultMaterial();


                Debug.Log("Chebyshev: " + chebyshev(tempChar_x, target_y, tempChar_x, target_y));
                enegy -= chebyshev(tempChar_x, target_x, tempChar_y, target_y);

                tempObjSelectCharacter = null;
                select = false;
                pathFinder.ResetPathBFS();
            }
        }
    }

    public void MouseSelectCharacter(ref bool select, PathFinder pathFinder, Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Character")) && hit.collider.gameObject.GetComponent<Character>() != null &&
                hit.collider.gameObject.GetComponent<Character>().group == player.playerName)
            {

                if (tempObjSelectCharacter != null)
                {
                    tempObjSelectCharacter.GetComponent<SetMaterial>().SetDefaultMaterial();
                    Debug.Log("Set Default Material to tempCharacter");
                }

                select = true;
                hit.collider.GetComponent<SetMaterial>().SetNewMaterial(aMouseSelectMaterial);
                tempObjSelectCharacter = hit.collider.gameObject;

                Debug.Log("Select: " + hit.collider.gameObject.name);
            }
            else /*if(Physics.Raycast(ray, out hit, Mathf.Infinity))*/
            {
                if (tempObjSelectCharacter != null)
                {
                    select = false;
                    pathFinder.ResetPathBFS();
                    tempObjSelectCharacter.GetComponent<SetMaterial>().SetDefaultMaterial();
                    tempObjSelectCharacter = null;
                    Debug.Log("select is false");
                }
            }

            if (tempObjSelectCharacter != null)
            {
                pathFinder.ResetPathBFS();
                pathFinder.PathFinding(tempObjSelectCharacter.GetComponent<Character>().x,
                                   tempObjSelectCharacter.GetComponent<Character>().y,
                                   GameData.instance.myEnergy);
            }
        }
    }

    public void ShowMouseOverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Character")) || Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")))
        {

            if (tempObjMouseOverObj != null)
            {
                tempObjMouseOverObj.GetComponent<SetMaterial>().SetDefaultMaterial();
            }

            tempObjMouseOverObj = hit.collider.gameObject;
            hit.collider.GetComponent<Renderer>().sharedMaterial = aMouseOverMaterial;

            //Debug.Log("Mouse Over: " + hit.collider.gameObject.name);
        }
        else
        {
            if (tempObjMouseOverObj != null)
            {
                tempObjMouseOverObj.GetComponent<SetMaterial>().SetDefaultMaterial();
            }
        }
    }

    int chebyshev(int herox, int monx, int heroy, int mony)
    {
        int result;
        if (Mathf.Abs(monx - herox) > Mathf.Abs(mony - heroy))
            result = Mathf.Abs(monx - herox);
        else result = Mathf.Abs(mony - heroy);

        return result;
    }
}

