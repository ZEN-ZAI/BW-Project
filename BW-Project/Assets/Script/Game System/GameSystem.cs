using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public bool delayLoad;
    public bool delayGet;
    public bool delayUp;

    public Player player;

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
        if (!delayLoad)
        {
            StartCoroutine(GetDelay(1));
        }
        

        if (!GameData.instance.End)
        {
            if (GameData.instance.myName == GameData.instance.q)
            {
                player.StartTurn();
            }

            CheckEndGame();
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
            UpdateMap();
            GameData.instance.q = GameData.instance.myName;
            StartCoroutine(NetworkSystem.instance.Enqueue(GameData.instance.q));

        }
        else if (!GameData.instance.firstPlayer)
        {
            player.SetSecond();

            if (!delayLoad)
            {
                //StartCoroutine(LoadDelay(1));
                LoadMap();
            }
        }
    }


    public IEnumerator LoadDelay(int sec)
    {
        delayLoad = true;
        yield return new WaitForSeconds(sec);
        delayLoad = false;
        LoadMap();
    }

    public IEnumerator GetDelay(int sec)
    {
        delayGet = true;
        yield return new WaitForSeconds(sec);
        delayGet = false;
        StartCoroutine(NetworkSystem.instance.GetData());
    }

    
    public IEnumerator UpdateDelay(int sec)
    {
        delayUp = true;
        yield return new WaitForSeconds(sec);
        delayUp = false;
        UpdateMap();
    }


    public void LoadMap()
    {
        StartCoroutine(NetworkSystem.instance.LoadMap());
    }

    public void UpdateMap()
    {
        StartCoroutine(NetworkSystem.instance.UpdateMap());
    }

    public void NextQueue()
    {
        // อัพ queue ขึ้น room
        GameData.instance.q = GameData.instance.enemyName;
        StartCoroutine(NetworkSystem.instance.Enqueue(GameData.instance.q));
        
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
