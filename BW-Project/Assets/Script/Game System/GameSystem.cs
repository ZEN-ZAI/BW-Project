using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;
    public string playerWin;
    public static GameSystem instance;

    public bool setup;

    public bool getDate;
    public bool loadCharacter;

    public bool moveCharacter;

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
            StartCoroutine(NetworkSystem.instance.GetData(done =>
            {
                if (done)
                {
                    getDate = false;

                    /*if (GameData.instance.q == GameData.instance.enemyID)
                    {
                        GameData.instance.myTurn = false;
                        GameData.instance.enemyTurn = true;
                    }*/
                }
            }));
        }
        if (setup)
        {


            //check turn
            if (GameData.instance.q == GameData.instance.myID && player.active == player.Waiting)
            {
                GameData.instance.enemyTurn = false;
                loadCharacter = true;
                player.StartTurn();
                NetworkSystem.instance.LoadCharacter(done =>
                {
                    if (done)
                    {
                        loadCharacter = false;
                        if (GameData.instance.leaderCharacter != null && 
                            GameData.instance.leaderCharacter.GetComponent<Character>().group != GameData.instance.myID)
                        {
                            NetworkSystem.instance.UpdateColumn("state", "END");
                            playerWin = "Player<" + GameData.instance.enemyID + "> : WIN";
                            UserInterfaceLink.instance.textEND.text = "YOU LOSE! Your leader is gone.";
                            Debug.Log(playerWin);
                        }
                    }
                });
            }
            else
            if (GameData.instance.q == GameData.instance.enemyID)
            {
                GameData.instance.myTurn = false;
                GameData.instance.enemyTurn = true;
            }

            //player Wait
            if (GameData.instance.state == "playing" && setup)
            {
            }


            if (GameData.instance.state == "move" && !moveCharacter)
            {
                if (GameData.instance.q.Contains("from"))
                {
                    moveCharacter = true;
                    tempFromWhere = GameData.instance.q.Split('|');

                    Debug.LogWarning(tempFromWhere[0]);
                    Debug.LogWarning(tempFromWhere[1]);

                    int tempFrom_x; int.TryParse(GetDataValue(tempFromWhere[0], "X:"), out tempFrom_x);
                    int tempFrom_y; int.TryParse(GetDataValue(tempFromWhere[0], "Y:"), out tempFrom_y);


                    int tempWhere_x; int.TryParse(GetDataValue(tempFromWhere[1], "X:"), out tempWhere_x);
                    int tempWhere_y; int.TryParse(GetDataValue(tempFromWhere[1], "Y:"), out tempWhere_y);

                    Debug.LogWarning("From: " + tempFrom_x + " " + tempFrom_y + " Where: " + tempWhere_x + " " + tempWhere_y);

                    Map.instance.map[tempFrom_y, tempFrom_x].character.GetComponent<Character>().WalkToBlock(tempWhere_x, tempWhere_y);
                }
            }

            if (GameData.instance.state == "END")
            {
                CheckEndGame();
                GameData.instance.myTurn = false;
                GameData.instance.enemyTurn = false;
            }
        }

    }

    public string[] tempFromWhere;
    public string tempWhere;

    private string nameMapMedium;
    private string nameMapLarge;

    private void SetUpMap()
    {

        StartCoroutine(NetworkSystem.instance.LoadElement(GameData.instance.mapName, done =>
        {
            if (done)
            {
                StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 =>
                {

                    if (done2)
                    {
                        setup = true;
                        LoadingScene.instance.LoadingScreen(false);
                        if (GameData.instance.firstPlayer)
                        {
                            Debug.LogWarning("first play enqueue");
                            NetworkSystem.instance.Enqueue(GameData.instance.myID);
                            NetworkSystem.instance.UpdateColumn("state", "playing");
                            player.StartTurn();
                        }

                        if (GameData.instance.firstPlayer)
                        {
                            NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.myID).ToString());
                            NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.enemyID).ToString());
                        }
                        else
                        {
                            NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.myID).ToString());
                            NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.enemyID).ToString());
                        }

                    }
                }));

            }
        }));
        /*
        if (GameData.instance.mapSize == 25)
        {
            StartCoroutine(NetworkSystem.instance.LoadElement(GameData.instance.mapName, done =>
             {
                 if (done)
                 {
                     StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 =>
                     {

                         if (done2)
                         {
                             setup = true;
                             LoadingScene.instance.LoadingScreen(false);
                             if (GameData.instance.firstPlayer)
                             {
                                 Debug.LogWarning("first play enqueue");
                                 NetworkSystem.instance.Enqueue(GameData.instance.myID);
                                 NetworkSystem.instance.UpdateColumn("state", "playing");
                                 player.StartTurn();
                             }

                             if (GameData.instance.firstPlayer)
                             {
                                 NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.myID).ToString());
                                 NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.enemyID).ToString());
                             }
                             else
                             {
                                 NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.myID).ToString());
                                 NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.enemyID).ToString());
                             }

                         }
                     }));

                 }
             }));
        }
        else if (GameData.instance.mapSize == 30)
        {

            StartCoroutine(NetworkSystem.instance.LoadElement(GameData.instance.mapName, done =>
            {
                if (done)
                {
                    StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 =>
                    {

                        if (done2)
                        {
                            setup = true;
                            LoadingScene.instance.LoadingScreen(false);
                            if (GameData.instance.firstPlayer)
                            {
                                Debug.LogWarning("first play enqueue");
                                NetworkSystem.instance.Enqueue(GameData.instance.myID);
                                NetworkSystem.instance.UpdateColumn("state", "playing");
                                player.StartTurn();
                            }

                            if (GameData.instance.firstPlayer)
                            {
                                NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.myID).ToString());
                                NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.enemyID).ToString());
                            }
                            else
                            {
                                NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.myID).ToString());
                                NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.enemyID).ToString());
                            }

                        }
                    }));
                }
            }));
        }
        else if (GameData.instance.mapSize == 50)
        {

            StartCoroutine(NetworkSystem.instance.LoadElement(GameData.instance.mapName, done =>
            {
                if (done)
                {
                    StartCoroutine(NetworkSystem.instance.LoadCharacter(done2 =>
                    {

                        if (done2)
                        {
                            setup = true;
                            LoadingScene.instance.LoadingScreen(false);
                            if (GameData.instance.firstPlayer)
                            {
                                Debug.LogWarning("first play enqueue");
                                NetworkSystem.instance.Enqueue(GameData.instance.myID);
                                NetworkSystem.instance.UpdateColumn("state", "playing");
                                player.StartTurn();
                            }

                            if (GameData.instance.firstPlayer)
                            {
                                NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.myID).ToString());
                                NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.enemyID).ToString());
                            }
                            else
                            {
                                NetworkSystem.instance.UpdateColumn("player2_people", CalculatePeople(GameData.instance.myID).ToString());
                                NetworkSystem.instance.UpdateColumn("player1_people", CalculatePeople(GameData.instance.enemyID).ToString());
                            }

                        }
                    }));
                }
            }));
        }*/
    }

    public void CheckEndGame()
    {
        if (!loadCharacter)
        {
            GameData.instance.myAllPeople = CalculatePeople(GameData.instance.myID);
            GameData.instance.enemyAllPeople = CalculatePeople(GameData.instance.enemyID);

            if (CalculatePeople("Npc") == 0)
            {

                if (GameData.instance.myAllPeople > GameData.instance.enemyAllPeople)
                {
                    playerWin = "Player<" + GameData.instance.myID + "> : WIN";
                    UserInterfaceLink.instance.textEND.text = "YOU WIN!";
                }
                else if (GameData.instance.enemyAllPeople > GameData.instance.myAllPeople)
                {
                    playerWin = "Player<" + GameData.instance.enemyID + "> : WIN";
                    UserInterfaceLink.instance.textEND.text = "YOU LOSE!";
                }
                else if (GameData.instance.enemyAllPeople == GameData.instance.myAllPeople)
                {
                    UserInterfaceLink.instance.textEND.text = "- DRAW -";
                }
                NetworkSystem.instance.UpdateColumn("state", "END");
            }
        }
    }


    public void NextQueue()
    {
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

    public void LoadCharacter()
    {
        StartCoroutine(NetworkSystem.instance.LoadCharacter(done => { }));
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

    private string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(".")) value = value.Remove(value.IndexOf("."));
        return value;
    }

}
