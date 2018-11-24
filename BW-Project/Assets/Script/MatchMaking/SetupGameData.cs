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

        for (int i = 0; i < player; i++)
        {
            Spawn(GameData.instance.myID.ToString());
            Spawn(GameData.instance.enemyID.ToString());
        }

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


}
