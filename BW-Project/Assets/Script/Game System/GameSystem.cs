using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;
    public string playerWin;
    private bool setup;
    public static GameSystem instance;

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
        GameSetUp();
    }

    void Update()
    {
        if (!GameData.instance.firstPlayer && !setup)
        {
            if (CalculatePeople("Npc") != 0)
            {
                setup = true;
            }
        }
        if (!NetworkSystem.instance.getData_isRuning && setup == true)
        {
            NetworkSystem.instance.GetData();
        }

        if (GameData.instance.q == "END" && playerWin != GameData.instance.myName)
        {
            if (!NetworkSystem.instance.loadMap_isRuning)
            {
                NetworkSystem.instance.LoadMap();
            }
            GameData.instance.myAllPeople = CalculatePeople(GameData.instance.myName);
            GameData.instance.enemyAllPeople = CalculatePeople(GameData.instance.enemyName);
            NetworkSystem.instance.Enqueue("END");
            CheckEndGame();
        }

        if (setup == true && !GameData.instance.End)
        {
            if (player.active == player.Waiting)
            {

                if (!NetworkSystem.instance.loadMap_isRuning)
                {
                    NetworkSystem.instance.LoadMap();
                }
            }

            if (GameData.instance.myName == GameData.instance.q && player.active == player.Waiting)
            {
                player.StartTurn();
            }

            CheckEndGame();
        }
    }

    public void GameSetUp()
    {
        GenerateMap.instance.Generate();

        if (GameData.instance.firstPlayer)
        {
            player.SetFrist();
            Spawner.instance.RandomSpawnNPC(GameData.instance.mapSize / 2);
            Spawner.instance.RandomSpawnCharacter(GameData.instance.myName, GameData.instance.myCharacterName, 3);
            Spawner.instance.RandomSpawnCharacter(GameData.instance.enemyName, GameData.instance.enemyCharacterName, 3);
            NetworkSystem.instance.UpdateMap();
            GameData.instance.q = GameData.instance.myName;
            NetworkSystem.instance.Enqueue(GameData.instance.q); // อัพ queue แรกขึ้น room
            setup = true;
        }
        else if (!GameData.instance.firstPlayer)
        {
            player.SetSecond();
            NetworkSystem.instance.LoadMap();
        }
    }

    public void NextQueue()
    {
        // อัพ queue ขึ้น room
        GameData.instance.q = GameData.instance.enemyName;
        NetworkSystem.instance.Enqueue(GameData.instance.q);

    }

    public void CheckEndGame()
    {
        int num = 0;

        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                if (Map.instance.map[i, j].HaveCharacter())
                {
                    if (Map.instance.map[i, j].character.GetComponent<Character>().group == "Npc")
                    {
                        num++;
                    }
                }
            }
        }

        if (num == 0)
        {
            GameData.instance.End = true;
            GameData.instance.myTurn = false;

            if (GameData.instance.myAllPeople > GameData.instance.enemyAllPeople)
            {
                playerWin = "Player<" + GameData.instance.myName + "> : WIN";
                Debug.Log(playerWin);
            }
            else if (GameData.instance.enemyAllPeople > GameData.instance.myAllPeople)
            {
                playerWin = "Player<" + GameData.instance.enemyName + "> : WIN";
                Debug.Log(playerWin);
            }
            else
            {
                playerWin = "- Draw -";
                Debug.Log(playerWin);
            }
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
        GameData.instance.End = false;
        GameData.instance.waitingPlayer = false;
        GameData.instance.mapSize = 0;

        GameData.instance.roomID = "";
        GameData.instance.q = "";
        GameData.instance.K = 0;
        GameData.instance.firstPlayer = false;

        //myName = "";
        GameData.instance.myCharacterName = "";
        GameData.instance.myAllPeople = 0;
        GameData.instance.myEnergy = 0;
        GameData.instance.myTurn = false;

        GameData.instance.enemyName = "";
        GameData.instance.enemyCharacterName = "";
        GameData.instance.enemyAllPeople = 0;
        GameData.instance.enemyEnergy = 0;
        LoadingScene.instance.LoadScene("MatchMaking");
    }

}
