using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;
    public string playerWin;
    public static GameSystem instance;

    public bool getDate;
    public bool loadCharacter;
    public bool firstQueue;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadingScene.instance.LoadingScreen(true);
        SetUpMap();
    }

    void Update()
    {
        if (!getDate)
        {
            getDate = true;
            StartCoroutine(NetworkSystem.instance.GetData(done => { if (done) { getDate = false; } }));

            if (GameData.instance.q == GameData.instance.enemyID)
            {
                GameData.instance.myTurn = false;
                GameData.instance.enemyTurn = true;
            }
        }

        //check turn
        if (GameData.instance.q == GameData.instance.myID && !GameData.instance.myTurn)
        {
            player.StartTurn();
            GameData.instance.enemyTurn = false;
        }
        else if (GameData.instance.q == GameData.instance.enemyID)
        {
            GameData.instance.myTurn = false;
            GameData.instance.enemyTurn = true;
        }

        //player Wait
        if (GameData.instance.state == "playing")
        {
            //CheckEndGame();

            if (player.active == player.Waiting)
            {
                if (!loadCharacter)
                {
                    loadCharacter = true;
                    StartCoroutine(NetworkSystem.instance.LoadCharacter(done => { if (done) { loadCharacter = false; } }));
                }
            }
        }

        if (GameData.instance.state == "END")
        {
            GameData.instance.myTurn = false;
            GameData.instance.enemyTurn = false;
        }

    }

    private void SetUpMap()
    {
        if (GameData.instance.mapSize == 25)
        {
            StartCoroutine(NetworkSystem.instance.LoadElement("Small",done => 
            {
                if (done)
                {
                    StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 => { }));
                    LoadingScene.instance.LoadingScreen(false);
                    if (GameData.instance.firstPlayer)
                    {
                        Debug.LogWarning("first play enqueue");
                        NetworkSystem.instance.Enqueue(GameData.instance.myID);
                        NetworkSystem.instance.UpdateColumn("state", "playing");
                        player.StartTurn();
                        firstQueue = true;
                    }
                }
            }));
        }
        else if (GameData.instance.mapSize == 35)
        {
            StartCoroutine(NetworkSystem.instance.LoadElement("Medium", done =>
            {
                if (done)
                {
                    StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 => { }));
                    LoadingScene.instance.LoadingScreen(false);
                    if (GameData.instance.firstPlayer)
                    {
                        Debug.LogWarning("first play enqueue");
                        NetworkSystem.instance.Enqueue(GameData.instance.myID);
                        NetworkSystem.instance.UpdateColumn("state", "playing");
                        player.StartTurn();
                        firstQueue = true;
                    }
                }
            }));
        }
        else if (GameData.instance.mapSize == 50)
        {
            StartCoroutine(NetworkSystem.instance.LoadElement("Large", done =>
            {
                if (done)
                {
                    StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 => { }));
                    LoadingScene.instance.LoadingScreen(false);
                    if (GameData.instance.firstPlayer)
                    {
                        Debug.LogWarning("first play enqueue");
                        NetworkSystem.instance.Enqueue(GameData.instance.myID);
                        NetworkSystem.instance.UpdateColumn("state", "playing");
                        player.StartTurn();
                        firstQueue = true;
                    }
                }
            }));
        }
    }


    private void CheckEndGame()
    {
        int num = CalculatePeople("Npc");

        if (num == 0)
        {

            if (GameData.instance.myAllPeople > GameData.instance.enemyAllPeople)
            {
                playerWin = "Player<" + GameData.instance.myID + "> : WIN";
                Debug.Log(playerWin);
            }
            else if (GameData.instance.enemyAllPeople > GameData.instance.myAllPeople)
            {
                playerWin = "Player<" + GameData.instance.enemyID + "> : WIN";
                Debug.Log(playerWin);
            }
            else
            {
                playerWin = "- Draw -";
                Debug.Log(playerWin);
            }

            NetworkSystem.instance.UpdateColumn("state", "END");
        }
    }


    public void NextQueue()
    {
        // อัพ queue ขึ้น room
        GameData.instance.q = GameData.instance.enemyID;
        NetworkSystem.instance.Enqueue(GameData.instance.q);
    }


    public int CalculatePeople(string group)
    {
        int num = 0;

        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                if (Map.instance.map[i, j].HaveCharacter())
                {
                    if (Map.instance.map[i, j].character.GetComponent<Character>().group == group)
                    {
                        num++;
                    }
                }
            }
        }
        return num;
    }

    public void ResetClearData()
    {
        StopAllCoroutines();

        NetworkSystem.instance.DeleteRoom();

        GameData.instance.waitingPlayer = false;
        GameData.instance.mapSize = 0;

        GameData.instance.firstPlayer = false;
        GameData.instance.roomID = "";
        GameData.instance.q = "";
        GameData.instance.K = 0;

        GameData.instance.state = "";

        //myName = "";
        GameData.instance.myCharacterName = "";
        GameData.instance.myAllPeople = 0;
        GameData.instance.myEnergy = 0;
        GameData.instance.myTurn = false;

        GameData.instance.enemyID = "";
        GameData.instance.enemyName = "";
        GameData.instance.enemyCharacterName = "";
        GameData.instance.enemyAllPeople = 0;
        GameData.instance.enemyEnergy = 0;
        GameData.instance.enemyTurn = false;

        LoadingScene.instance.LoadScene("MatchMaking");
    }

}
