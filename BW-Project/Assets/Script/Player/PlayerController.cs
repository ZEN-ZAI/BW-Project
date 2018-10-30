using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool selectCharecter;

    private MouseScript mouseScript;
    private PathFinder pathFinder;

    // Start is called before the first frame update
    void Start()
    {
        mouseScript = FindObjectOfType<MouseScript>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameData.instance.myTurn)
        {

            if (!selectCharecter)
            {
                MouseOver();
                SelectCharecter();
            }
            else if (selectCharecter)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Path" && GameData.instance.energy > 0)
                {
                    MoveCharecter();

                }
                else
                {
                    SelectCharecter();
                }
            }
        }
        else
        {
            MouseOver();
        }
    }

    public void UesSkill()
    {

    }

    private void MouseOver()
    {
        mouseScript.ShowMouseOverObject();
    }

    private void SelectCharecter()
    {
        //mouseScript.MouseSelectCharacter(ref selectCharecter, pathFinder, GameData.instance.playerName);

    }
    private void MoveCharecter()
    {
        mouseScript.SelectToMove(ref selectCharecter, ref GameData.instance.energy, pathFinder);

    }
}
