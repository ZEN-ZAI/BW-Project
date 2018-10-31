using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public string characterPlayerName;
    public GameObject leaderCharacter;

    public int myAllPeople;
    public int energy;
    public bool myTurn;


    public MouseScript mouseScript;
    private PathFinder pathFinder;

    public bool selectCharecter;

    public delegate void state();
    public state active;

    // Start is called before the first frame update
    void Start()
    {
        //mouseScript = FindObjectOfType<MouseScript>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    // Update is called once per frame

    public void SetFrist()
    {
        energy = 5;
        active = (state)(Playing);
        active();
    }

    public void SetSecond()
    {
        energy = 6;
        active = (state)(Waiting);
        active();
    }

    void Update()
    {

        if (active == Playing && myTurn) //  to self
        {
            active = (state)(Playing);
            active();
        }
        else if (active == Playing && !myTurn) // Playing to Waiting
        {
            active = (state)(Waiting);
            active();
        }
        else if (active == Waiting && !myTurn) // to self
        {
            active = (state)(Waiting);
            active();
        }
        else if (active == Waiting && myTurn) // Waiting to Playing
        {
            active = (state)(Playing);
            active();
        }
        else if (active == Waiting && GameSystem.instance.End ) // Waiting to end
        {
            active = (state)(Waiting);
            active();
        }


        if (active == Playing && energy == 0)
        {
            EndTurn();
        }
    }

    private void Playing()
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

    private void Waiting()
    {

    }

    public void StartTurn()
    {
        energy = 5;
        myTurn = true;
    }

    public void UesSkill()
    {
        leaderCharacter.GetComponent<LeaderCharacter>().UseSkill();
    }



    public void EndTurn()
    {
        if (myTurn)
        {
            myTurn = false;
            KNN.instance.StartKNN();
            myAllPeople =  GameSystem.instance.HowManyMyPeople(playerName);
            GameSystem.instance.NextQueue();
        }
    }

    public void MouseOver()
    {
        mouseScript.ShowMouseOverObject();
    }

    private void SelectCharecter()
    {
        mouseScript.MouseSelectCharacter(ref selectCharecter, pathFinder, this);

    }
    private void MoveCharecter()
    {
        mouseScript.SelectToMove(ref selectCharecter, ref energy, pathFinder);

    }
}
