using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MouseScript mouseScript;
    private PathFinder pathFinder;

    public bool selectCharecter;

    public delegate void state();
    public state active;

    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        //myTurn = GameData.instance.myTurn;

        if (GameData.instance.firstPlayer)
        {
            SetFrist();
        }
        else if (GameData.instance.firstPlayer)
        {
            SetSecond();
        }
    }

    // Update is called once per frame

    public void SetFrist()
    {
        GameData.instance.myEnergy = 5;
        active = (state)(Playing);
        active();
    }

    public void SetSecond()
    {
        GameData.instance.myEnergy = 6;
        active = (state)(Waiting);
        active();
    }


    void Update()
    {

        if (active == Playing && GameData.instance.myTurn) //  to self
        {
            active = (state)(Playing);
            active();
        }
        else if (active == Playing && !GameData.instance.myTurn) // Playing to Waiting
        {
            active = (state)(Waiting);
            active();
        }
        else if (active == Waiting && !GameData.instance.myTurn) // to self
        {
            active = (state)(Waiting);
            active();
        }
        else if (active == Waiting && GameData.instance.myTurn) // Waiting to Playing
        {
            active = (state)(Playing);
            active();
        }
        else if (active == Waiting && GameData.instance.End) // Waiting to end
        {
            active = (state)(Waiting);
            active();
        }


        if (active == Playing && GameData.instance.myEnergy == 0)
        {
            EndTurn();
        }
    }

    public void Playing()
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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag == "Path")
            {
                MoveCharecter();
            }
            else
            {
                SelectCharecter();
            }
        }

    }

    public void Waiting()
    {
        MouseOver();
    }

    public void StartTurn()
    {
        GameData.instance.myEnergy = 5;
        GameData.instance.myTurn = true;

        NetworkSystem.instance.LoadMap(2);
    }

    public void UesSkill()
    {
        GameData.instance.leaderCharacter.GetComponent<LeaderCharacter>().UseSkill();
    }

    public void EndTurn()
    {
        if (GameData.instance.myTurn)
        {
            KNN.instance.StartKNN();
            NetworkSystem.instance.UpdateMap();
            GameSystem.instance.NextQueue();
            StartCoroutine(DelayEndTurn(1));
            GameData.instance.myEnergy = 0;
        }
    }

    private IEnumerator DelayEndTurn(int sec)
    {
        yield return new WaitForSeconds(1);
        GameData.instance.myTurn = false;
    }

    public void MouseOver()
    {
        mouseScript.ShowMouseOverObject();
    }

    private void SelectCharecter()
    {
        mouseScript.MouseSelectCharacter(ref selectCharecter, pathFinder);

    }
    private void MoveCharecter()
    {
        mouseScript.SelectToMove(ref selectCharecter, ref GameData.instance.myEnergy, pathFinder);
    }
}
