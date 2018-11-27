using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class SetupGameData : MonoBehaviour
{
    public InputField inputField_PlayerName;
    public TMP_Dropdown dropdown;
    public GameObject waitingScreen;

    public string[,] map;
    public int allCharacter;
    public int maxCharacter;

    private bool spawed;

    void Start()
    {
        int temp = Random.seed = (int)System.DateTime.Now.Ticks;
        GameData.instance.myID = Mathf.Abs(temp).ToString();
        Debug.Log("Player ID: " + GameData.instance.myID);
    }

    // Update is called once per frame
    void Update()
    {
        GameData.instance.myName = inputField_PlayerName.text;

        if (GameData.instance.waitingPlayer)
        {
            waitingScreen.SetActive(true);
        }
        else
        {
            waitingScreen.SetActive(false);
        }

        
        if (dropdown.value == 0)
        {
            NewMap(25);
        }
        else if (dropdown.value == 1)
        {
            NewMap(35);
        }
        else if (dropdown.value == 2)
        {
            NewMap(50);
        }
        else if (dropdown.value == 3)
        {
            NewMap(100);
        }

        //for P1
        if (GameData.instance.firstPlayer && GameData.instance.enemyID != "" && !spawed)
        {
            spawed = true;
            SpawnSetUp();
            StartCoroutine(SetUpMap());
        }

        //for P2
        if (GameData.instance.state == "setup_spawn")
        {
            StopAllCoroutines();
            LoadingScene.instance.LoadScene("Game");
        }
    }

    private void NewMap(int size)
    {
        GameData.instance.mapSize = size;
        map = new string[size, size];
        maxCharacter = size * size;
    }

    private void SpawnSetUp()
    {
        int npc = GameData.instance.mapSize/3;
        int player = 3;
        
        for (int i = 0; i < npc; i++)
        {
            Spawn("Npc");
        }

        SpawnLeader(GameData.instance.myID.ToString(), myleader_x, myleader_y);
        SpawnLeader(GameData.instance.enemyID.ToString(), enermyleader_x, enermyleader_y);

        for (int i = 0; i < player; i++)
        {
            SpawnClosed(GameData.instance.myID.ToString(), myleader_x, myleader_y);
            SpawnClosed(GameData.instance.enemyID.ToString(), enermyleader_x, enermyleader_y);
        }

    }

    int myleader_x; int myleader_y;
    int enermyleader_x; int enermyleader_y;

    private void SpawnClosed(string name, int target_x, int target_y)
    {

        int x = Random.Range(target_x-5, target_x+5);
        int y = Random.Range(target_y-5, target_y+5);


        while (x < 0
            || y < 0
            || x > GameData.instance.mapSize
            || y  > GameData.instance.mapSize
            || map[y, x] == name || map[y, x] == "Npc")
        {
            Debug.Log("Error: Spawn Repeated");
            x = Random.Range(target_x - 5, target_x + 5);
            y = Random.Range(target_y - 5, target_y + 5);
        }

        map[y, x] = name;
        allCharacter++;

        Debug.Log("" + name + " spawn On <X:" + x + " Y:" + y + ">");
    }

    private void SpawnLeader(string name, int pos_x, int pos_y)
    {

        int x = Random.Range(0, GameData.instance.mapSize);
        int y = Random.Range(0, GameData.instance.mapSize);

        while (map[y, x] == "Npc")
        {
            Debug.Log("Error: Spawn Repeated");
            x = Random.Range(0, GameData.instance.mapSize);
            y = Random.Range(0, GameData.instance.mapSize);
        }
        pos_x = x;
        pos_y = y;
        map[y, x] = name+"Leader";
        allCharacter++;

        Debug.Log("" + name + " spawn On <X:" + x + " Y:" + y + ">");
    }

    private void Spawn(string name)
    {

        int x = Random.Range(0, GameData.instance.mapSize);
        int y = Random.Range(0, GameData.instance.mapSize);

        while (map[y, x] == name)
        {
            Debug.Log("Error: Spawn Repeated");
            x = Random.Range(0, GameData.instance.mapSize);
            y = Random.Range(0, GameData.instance.mapSize);
        }

        map[y, x] = name;
        allCharacter++;

        Debug.Log(""+ name + " spawn On <X:" + x + " Y:" + y + ">");
    }

    public string tempMap;
    private IEnumerator SetUpMap()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", "zenzai");
        form.AddField("password", "541459%server");
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("mapSize", GameData.instance.mapSize);

        int num = 0;
        for (int i = 0; i < GameData.instance.mapSize; i++)
        {
            for (int j = 0; j < GameData.instance.mapSize; j++)
            {
                if (map[i, j] != "")
                {
                    tempMap += map[i, j] + "|";
                }
                else
                {
                    tempMap += "_" + "|";
                }
                num++;
            }
        }

        form.AddField("map", tempMap);

        UnityWebRequest www = UnityWebRequest.Post("http://www.brainwashgame.com/UpdateMap2.php", form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        if (itemsDataString == "")
        {
            Debug.Log("Connecting Error.");
        }
        else
        {
            Debug.Log(itemsDataString);
            MatchMakingSystem.instance.UpdateColumn("state","setup_spawn");
        }
    }

    private int chebyshev(int herox, int monx, int heroy, int mony)
    {
        int result;
        if (Mathf.Abs(monx - herox) > Mathf.Abs(mony - heroy))
            result = Mathf.Abs(monx - herox);
        else result = Mathf.Abs(mony - heroy);

        return result;
    }


}
