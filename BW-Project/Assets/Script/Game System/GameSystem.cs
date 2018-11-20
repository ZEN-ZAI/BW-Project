using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;
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
        if (player.active == player.Waiting)
        {
            Debug.Log("player.active == player.Waiting");
        }
        if (setup == true && !GameData.instance.End)
        {
            if (player.active == player.Waiting)
            {
                if (!NetworkSystem.instance.getData_isRuning)
                {
                    NetworkSystem.instance.GetData();
                }

                if (!NetworkSystem.instance.loadMap_isRuning)
                {
                    NetworkSystem.instance.LoadMap();
                }
            }

            if (!GameData.instance.End)
            {
                if (GameData.instance.myName == GameData.instance.q && player.active == player.Waiting)
                {
                    player.StartTurn();
                }

                CheckEndGame();
            }
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
            NetworkSystem.instance.GetData();

            if (!NetworkSystem.instance.loadMap_isRuning)
            {
                NetworkSystem.instance.LoadMap();
            }
            setup = true;
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
                Debug.Log("Player<" + GameData.instance.myName + "> : WIN");
            }
            else if (GameData.instance.enemyAllPeople > GameData.instance.myAllPeople)
            {
                Debug.Log("Player<" + GameData.instance.enemyName + "> : WIN");
            }
            else
            {
                Debug.Log(" - Draw -");
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



}
