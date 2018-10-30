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
        q.Peek().SetTurn(true);
        q.Enqueue(q.Dequeue());
    }

    public void GameSetUp()
    {
        SetSpawnNpc();
        SetSpawnCharacterPlayer();
        EnqueuePlayer();
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
            spawner.SpawnCharacter(item);
        }
    }

    public void EnqueuePlayer()
    {
        player[0].SetTurn(true);

        for (int i = 0; i < 2; i++)
        {
            q.Enqueue(player[i]);
        }
    }
}
