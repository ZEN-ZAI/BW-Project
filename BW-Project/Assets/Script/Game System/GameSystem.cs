using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player[] player;

    Queue<Player> q = new Queue<Player>();

    public Spawner spawner;

    public bool KNN_finish;

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
        spawner = FindObjectOfType<Spawner>();
        GameSetUp();
    }

    void Update()
    {
        if (!End)
        {
            CheckEndGame();
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
            foreach (var item in player)
            {
                item.myTurn = false;
            }

            if (player[0].myAllPeople > player[1].myAllPeople)
            {
                Debug.Log("Player<" + player[0].playerName + "> : WIN");
            }
            else if (player[1].myAllPeople > player[0].myAllPeople)
            {
                Debug.Log("Player<" + player[1].playerName + "> : WIN");
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

    public void NextQueue()
    {
        Player tempPlayer = q.Peek();
        tempPlayer.StartTurn();

        q.Enqueue(tempPlayer);
        q.Dequeue();
    }

    public void GameSetUp()
    {
        GenerateMap.instance.Generate();
        SetSpawnNpc();
        SetSpawnCharacterPlayer();
        EnqueuePlayer();
        NextQueue();
    }

    public void SetSpawnNpc()
    {
        for (int i = 0; i < (Map.instance.row * Map.instance.col) / 10; i++)
        {
            spawner.SpawnNPC();
        }
    }

    public void SetSpawnCharacterPlayer()
    {
        foreach (var item in player)
        {
            for (int i = 0; i < 3; i++)
            {
                spawner.SpawnCharacter(item);
            }
        }
    }

    public void EnqueuePlayer()
    {
        player[0].SetFrist();
        player[1].SetSecond();

        foreach (var item in player)
        {
            q.Enqueue(item);
        }
    }
}
