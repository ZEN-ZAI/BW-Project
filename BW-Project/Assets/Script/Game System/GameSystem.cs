using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;

    public string q;

    public static GameSystem instance;

    public bool End;

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
        if (!End)
        {
            CheckMyTurn();
            CheckEndGame();
        }
    }

    public void GameSetUp()
    {
        GenerateMap.instance.Generate();
        SetUpNpc();
        SetupMyCharacterPlayer();
        SetupEnemyCharacterPlayer();
        if (GameData.instance.firstPlayer)
        {
            NetworkSystem.instance.UpdateMap(Map.instance);
        }
        else
        {
            NetworkSystem.instance.LoadMap(Map.instance);
        }
    }

    public void SetUpNpc()
    {
        for (int i = 0; i < (Map.instance.row * Map.instance.col) / 10; i++)
        {
            Spawner.instance.SpawnNPC();
        }
    }

    public void SetupMyCharacterPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            Spawner.instance.SpawnCharacter(GameData.instance.myName, GameData.instance.myCharacterName);
        }
    }

    public void SetupEnemyCharacterPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            Spawner.instance.SpawnCharacter(GameData.instance.enemyName, GameData.instance.enemyCharacterName);
        }

    }
    public void NextQueue()
    {
        // อัพ queue ขึ้น room
        GameData.instance.q = GameData.instance.enemyName;
    }

    public void CheckMyTurn()
    {
        if (GameData.instance.myName == GameData.instance.q)
        {
            player.StartTurn();
        }
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
            End = true;
            player.myTurn = false;

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

    public int HowManyMyPeople(string group)
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
