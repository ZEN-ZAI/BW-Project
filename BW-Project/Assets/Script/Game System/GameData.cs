using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //MatchMaking
    public bool waitingPlayer;
    public int mapSize;
    //

    public bool online;
    
    public bool End;
    public string roomID;
    public string state;
    public string q;
    public int K;
    public bool firstPlayer;

    public int myID;
    public string myName;
    public string myCharacterName;
    public int myAllPeople;
    public int myEnergy;


    public string enemyID;
    public string enemyName;
    public string enemyCharacterName;
    public int enemyAllPeople;
    public int enemyEnergy;


    public bool myTurn;
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
