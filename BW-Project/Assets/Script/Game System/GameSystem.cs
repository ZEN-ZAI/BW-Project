using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;
    public string playerWin;
    public static GameSystem instance;
    private bool setup;

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
        GameSetUp();
    }

    void Update()
    {
        if (!NetworkSystem.instance.getData_isRuning)
        {
            NetworkSystem.instance.GetData();
        }

        if (GameData.instance.state == "END" && !GameData.instance.End)
        {
            GameData.instance.End = true;
            NetworkSystem.instance.LoadMap();
            
        }

        if (!GameData.instance.End)
        {
            if (player.active == player.Waiting)
            {
                if (!NetworkSystem.instance.loadMap_isRuning)
                {
                    NetworkSystem.instance.LoadMap();
                }
            }

            if (GameData.instance.myID == GameData.instance.q && player.active == player.Waiting)
            {
                player.StartTurn();
            }

            CheckEndGame();
        }
    }

    public void GameSetUp()
    {
        GenerateMap.instance.Generate();
        NetworkSystem.instance.LoadMap();

        if (GameData.instance.firstPlayer)
        {
            player.SetFrist();
        }
        else if (!GameData.instance.firstPlayer)
        {
            player.SetSecond();
        }

        LoadingScene.instance.LoadingScreen(false);
        NetworkSystem.instance.UpdateColumn("state","setup_finish");
        setup = true;
    }

    public void NextQueue()
    {
        // อัพ queue ขึ้น room
        GameData.instance.q = GameData.instance.enemyID;
        NetworkSystem.instance.Enqueue(GameData.instance.q);

    }

    public void CheckEndGame()
    {
        int num = CalculatePeople("Npc");

        if (num == 0)
        {
            GameData.instance.End = true;

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

            NetworkSystem.instance.UpdateColumn("state","END");
        }
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
        GameData.instance.End = false;

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
