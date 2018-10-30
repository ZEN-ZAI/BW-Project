using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public string roomID;
    public int K;
    public int energy;

    public string playerName;
    public string characterPlayerName;

    public string enemyName;
    public string characterEnemyName;

    public bool firstPlayer;

    public GameObject npc;
    public GameObject characterPlayer;
    public GameObject characterEnemy;

    public static GameData instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
