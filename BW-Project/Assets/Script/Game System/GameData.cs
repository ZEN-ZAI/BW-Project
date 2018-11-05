using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public string roomID;
    public int K;
    public int energy;
    public bool myTurn;

    public int playerID;
    public string playerName;
    public string characterPlayerName;

    public int enemyID;
    public string enemyName;
    public string characterEnemyName;

    public bool firstPlayer;

    public GameObject leaderCharacter;

    public GameObject npc;
    public GameObject characterPlayer;
    public GameObject characterEnemy;

    public static GameData instance;

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
