using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //MatchMaking
    public bool waitingPlayer;
    public int mapSize;
    public string mapName;
    //

    public bool online;
    
    public bool firstPlayer;
    public string roomID;
    public string q;
    public int K;
    
    public string state;

    public string myID;
    public string myName;
    public string myCharacterName;
    public int myAllPeople;
    public int myEnergy;
    public bool myTurn;

    public string enemyID;
    public string enemyName;
    public string enemyCharacterName;
    public int enemyAllPeople;
    public int enemyEnergy;
    public bool enemyTurn;

    public GameObject leaderCharacter;

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
