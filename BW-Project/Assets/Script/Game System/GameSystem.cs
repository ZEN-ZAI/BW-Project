using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player[] player;

    Queue<Player> q = new Queue<Player>();

    public Spawner spawner;

    public bool KNN_finish = true;

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
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        GameSetUp();
    }

    public void NextQueue()
    {
        Player tempPlayer = q.Peek();
        tempPlayer.SetTurn();

        q.Enqueue(tempPlayer);
        q.Dequeue();
    }

    public void GameSetUp()
    {
        SetSpawnNpc();
        SetSpawnCharacterPlayer();
        EnqueuePlayer();
        NextQueue();
    }

    public void SetSpawnNpc()
    {
        for (int i = 0; i < (Map.instance.row* Map.instance.col)/10 ; i++)
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
