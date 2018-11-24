using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        else if (active == Waiting && GameData.instance.state == "END") // Waiting to end
        {
            active = (state)(Waiting);
            active();
        }


        if (active == Playing && GameData.instance.myEnergy == 0)
        {
            EndTurn();
        }
    }

    public Material aMouseOverMaterial;
    public Material aMouseSelectMaterial;

    public GameObject tempObjSelectCharacter;
    private GameObject tempObjMouseOverObj;

    public void Playing()
    {
        if (!selectCharecter)
        {
            ShowMouseOverObject();
        }
        if (selectCharecter && Input.GetMouseButton(1))
        {
            CancelSelectCharacter();
        }


        if (!selectCharecter && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Character")) && hit.collider.gameObject.GetComponent<Character>() != null &&
                hit.collider.gameObject.GetComponent<Character>().group == GameData.instance.myID)
            {
                FirstSelectCharacter(hit);

                CameraMove.instance.MoveToPoint(tempObjSelectCharacter.transform.position);
                CameraZoom.instance.ZoomIn();
            }
        }
        else if (selectCharecter && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")) && hit.collider.GetComponent<Tile>() != null
                && hit.collider.GetComponent<Tile>().pathLevel > 0)
            {
                SelectToMove(hit);

            }
            else
            if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Character")) && hit.collider.gameObject.GetComponent<Character>() != null
                && hit.collider.gameObject.GetComponent<Character>().group == GameData.instance.myID)
            {
                NewSelectCharacter(hit);
                CameraMove.instance.MoveToPoint(tempObjSelectCharacter.transform.position);
                CameraZoom.instance.ZoomIn();
            }
            else /*if(Physics.Raycast(ray, out hit, Mathf.Infinity))*/
            {
                if (tempObjSelectCharacter != null)
                {
                    CancelSelectCharacter();
                }
            }
        }
    }

    private void CancelSelectCharacter()
    {
        selectCharecter = false;
        pathFinder.ResetPathBFS();
        tempObjSelectCharacter.GetComponent<SetMaterial>().SetDefaultMaterial();
        tempObjSelectCharacter = null;
        Debug.Log("select is false");
    }

    private void SelectToMove(RaycastHit hit)
    {
        int tempChar_x = tempObjSelectCharacter.GetComponent<Character>().x; int target_x = hit.collider.GetComponent<Tile>().col;
        int tempChar_y = tempObjSelectCharacter.GetComponent<Character>().y; int target_y = hit.collider.GetComponent<Tile>().row;

        tempObjSelectCharacter.GetComponent<Character>().WalkToBlock(hit.collider.GetComponent<Tile>().col, hit.collider.GetComponent<Tile>().row);
        tempObjSelectCharacter.GetComponent<SetMaterial>().SetDefaultMaterial();


        Debug.Log("Chebyshev: " + chebyshev(tempChar_x, target_y, tempChar_x, target_y));
        GameData.instance.enemyEnergy -= chebyshev(tempChar_x, target_x, tempChar_y, target_y);
        StartCoroutine(NetworkSystem.instance.UpdateCharacter());
        if (GameData.instance.firstPlayer)
        {
            NetworkSystem.instance.UpdateColumn("player1_energy", GameData.instance.myEnergy.ToString());
        }
        else
        {
            NetworkSystem.instance.UpdateColumn("player2_energy", GameData.instance.myEnergy.ToString());
        }

        tempObjSelectCharacter = null;
        selectCharecter = false;
        pathFinder.ResetPathBFS();
    }

    private void FirstSelectCharacter(RaycastHit hit)
    {
        hit.collider.GetComponent<SetMaterial>().SetNewMaterial(aMouseSelectMaterial);
        tempObjSelectCharacter = hit.collider.gameObject;

        pathFinder.ResetPathBFS();
        pathFinder.PathFinding(tempObjSelectCharacter.GetComponent<Character>().x,
                           tempObjSelectCharacter.GetComponent<Character>().y,
                           GameData.instance.myEnergy);

        selectCharecter = true;
        Debug.Log("Select: " + hit.collider.gameObject.name);
    }

    private void NewSelectCharacter(RaycastHit hit)
    {
        tempObjSelectCharacter.GetComponent<SetMaterial>().SetDefaultMaterial(); Debug.Log("Set Default Material to tempCharacter");

        selectCharecter = true;
        hit.collider.GetComponent<SetMaterial>().SetNewMaterial(aMouseSelectMaterial);
        tempObjSelectCharacter = hit.collider.gameObject;

        pathFinder.ResetPathBFS();
        pathFinder.PathFinding(tempObjSelectCharacter.GetComponent<Character>().x,
                           tempObjSelectCharacter.GetComponent<Character>().y,
                           GameData.instance.myEnergy);

        Debug.Log("New Select: " + hit.collider.gameObject.name);
    }

    private void ShowMouseOverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Character")) || Physics.Raycast(ray, out hit, LayerMask.GetMask("Tile")))
        {
            if (hit.collider.GetComponent<Tile>() != null || hit.collider.GetComponent<Character>() != null)
            {
                if (tempObjMouseOverObj != null)
                {
                    tempObjMouseOverObj.GetComponent<SetMaterial>().SetDefaultMaterial();
                }

                tempObjMouseOverObj = hit.collider.gameObject;
                hit.collider.GetComponent<Renderer>().sharedMaterial = aMouseOverMaterial;
                //Debug.Log("Mouse Over: " + hit.collider.gameObject.name);
            }
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

    public void Waiting()
    {
        ShowMouseOverObject();
    }

    public void StartTurn()
    {
        GameData.instance.myEnergy = 5;
        GameData.instance.myTurn = true;
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
            StartCoroutine(NetworkSystem.instance.UpdateCharacter());

            GameData.instance.myAllPeople = GameSystem.instance.CalculatePeople(GameData.instance.myID);
            GameData.instance.enemyAllPeople = GameSystem.instance.CalculatePeople(GameData.instance.enemyID);

            if (GameData.instance.firstPlayer)
            {
                NetworkSystem.instance.UpdateColumn("player1_people", GameData.instance.myAllPeople.ToString());
                NetworkSystem.instance.UpdateColumn("player2_people", GameData.instance.enemyAllPeople.ToString());
            }
            else
            {
                NetworkSystem.instance.UpdateColumn("player2_people", GameData.instance.myAllPeople.ToString());
                NetworkSystem.instance.UpdateColumn("player1_people", GameData.instance.enemyAllPeople.ToString());
            }

            GameSystem.instance.NextQueue();

            GameData.instance.myTurn = false;
            GameData.instance.myEnergy = 0;
        }
    }

    private IEnumerator DelayEndTurn(int sec)
    {
        yield return new WaitForSeconds(1);
        GameData.instance.myTurn = false;
    }
}
