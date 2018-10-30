using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player[] player;

    public Player playerNow;
    Queue<Player> q = new Queue<Player>();

    public Spawner spawner;

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


    // Update is called once per frame
    void Update()
    {
        
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
        foreach (var item in player)
        {
            q.Enqueue(item);
        }
    }
}
