using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSystem : MonoBehaviour {

    private string username = "zenzai";
    private string password = "541459%server";
    private string database_IP = "http://www.brainwashgame.com";
    private string getData = "/GetData.php";
    private string updateMap = "/UpdateMap2.php";
    private string loadMap = "/LoadMap.php";
    private string enqueue = "/EnQueue.php";
    private string updateState = "/UpdateStatement.php";

    private string deleteRoom = "/DeleteRoom.php";

    public string[] tempData;

    public bool loadMap_isRuning;
    public bool updateMap_isRuning;
    public bool getData_isRuning;

    public static NetworkSystem instance;

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

    public void DeleteRoom()
    {
        StartCoroutine(DeleteRoom(database_IP+deleteRoom));
    }

    private IEnumerator DeleteRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error, Can't delete room.");
        }
        else
        {
            Debug.Log("Connecting Succeeded, " + www.downloadHandler.text);
            GameData.instance.roomID = "";
        }

    }

    public void LoadMap()
    {
        StartCoroutine(_LoadMap());
    }

    public void LoadElement(string mapName)
    {
        StartCoroutine(_LoadElement(mapName));
    }

    public void UpdateMap()
    {
        StartCoroutine(_UpdateMap());
    }

    public void GetData()
    {
        StartCoroutine(_GetData());
    }

    public void UpdateColumn(string column, string statement)
    {
        StartCoroutine(_UpdateColumn(column, statement));
    }

    public void Enqueue(string name)
    {
        StartCoroutine(_Enqueue(name));
    }

    public void UpdateMap(int sec)
    {
        StartCoroutine(UpdateDelay(sec));
    }

    public void LoadMap(int sec)
    {
        StartCoroutine(LoadDelay(sec));
    }

    public void GetData(int sec)
    {
        StartCoroutine(GetDataDelay(sec));
    }
    public string[,] tempCharacter;
    private IEnumerator _LoadMap()
    {
        tempCharacter = new string[GameData.instance.mapSize, GameData.instance.mapSize];
        loadMap_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + loadMap, form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        if (itemsDataString == "")
        {
            Debug.Log("Connecting Error.");
        }
        else
        {
            Debug.Log("Load character.");
            Debug.Log(itemsDataString);
        }

        tempData = itemsDataString.Split(';');
        int num = 0;

        for (int row = 0; row < Map.instance.row; row++)
        {
            for (int col = 0; col < Map.instance.col; col++)
            {

                tempCharacter[row, col] = tempData[num];

                if (Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Map.instance.DestroyCharacter(col, row);
                    Debug.Log("Destroy obj <x:" + col + " y:" + row + ">");
                }

                if (tempCharacter[row, col] == "Npc" && !Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Spawner.instance.SpawnNPC(col, row);
                    //Debug.Log("Load npc <x:" + col + " y:" + row + ">");
                }else

                if(tempCharacter[row, col] == GameData.instance.myID && !Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Spawner.instance.SpawnCharacter(GameData.instance.myID, GameData.instance.myCharacterName, col, row);
                    //Debug.Log("Load myPeople <x:" + col + " y:" + row + ">");
                }else

                if(tempCharacter[row, col] == GameData.instance.enemyID && !Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Spawner.instance.SpawnCharacter(GameData.instance.enemyID, GameData.instance.enemyCharacterName, col, row);
                    //Debug.Log("Load enemy <x:" + col + " y:" + row + ">");
                }
                num++;
            }
        }
        
        loadMap_isRuning = false;
    }

    public string tempMap;
    private IEnumerator _UpdateMap()
    {
        updateMap_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("mapSize", GameData.instance.mapSize);

        tempMap = null;

        int num = 0;
        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                if (Map.instance.map[i, j].GetComponent<Tile>().HaveCharacter())
                {
                    tempMap += Map.instance.map[i, j].GetComponent<Tile>().character.GetComponent<Character>().group + "|";
                }
                else
                {
                    tempMap += "_" + "|";
                }
                num++;
            }
        }

        form.AddField("map",tempMap);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + updateMap ,form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        if (itemsDataString == "")
        {
            Debug.Log("Connecting Error.");
        }
        else
        {
            Debug.Log(itemsDataString);
        }
        updateMap_isRuning = false;
    }

    private IEnumerator _LoadElement(string mapName)
    {
        string[,] tempMap = new string[GameData.instance.mapSize, GameData.instance.mapSize];
        loadMap_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", mapName);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + loadMap, form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        if (itemsDataString == "")
        {
            Debug.Log("Connecting Error.");
        }
        else
        {
            Debug.Log("Load Element.");
            
        }
        itemsDataString = itemsDataString.Remove(itemsDataString.Length-1);
        Debug.Log(itemsDataString);

        tempData = itemsDataString.Split(';');

        int num = 0;
        for (int row = 0; row < GameData.instance.mapSize; row++)
        {
            for (int col = 0; col < GameData.instance.mapSize; col++)
            {

                tempMap[row, col] = tempData[num];

                if (tempMap[row, col].Contains("Building_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(0, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(1, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_C"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(2, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_D"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(3, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_E"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(4, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_F"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(5, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_G"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(6, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_H"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(7, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(8, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(9, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_C"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(10, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_D"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(11, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_Sand_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(12, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_Sand_Desert_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(13, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_Sand_Desert_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(14, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_water_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(15, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_water_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(16, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_water_C"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(17, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Intersection_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(19, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Intersection_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(20, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_ThirdStreed"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(21, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Turn_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(22, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Turn_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(23, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Rock_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(24, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Tree_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(25, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Tree_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(26, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                else if (tempMap[row, col].Contains("Road"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateMap.instance.GenerateBlock(18, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, temp);
                    GenerateMap.instance.positionZ++;
                }
                /*else
                {
                    GenerateMap.instance.GenerateBlock(8, new Vector3(GenerateMap.instance.positionX, 0, GenerateMap.instance.positionZ), row, col, 0);
                    GenerateMap.instance.positionZ++;
                }*/

                num++;
            }
            GenerateMap.instance.positionX++;
            GenerateMap.instance.positionZ = 0;
        }

        loadMap_isRuning = false;
    }



    private IEnumerator _GetData()
    {
        getData_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + getData, form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;
        Debug.Log(itemsDataString);

        GameData.instance.q = GetDataValue(itemsDataString, "queue:");
        GameData.instance.state = GetDataValue(itemsDataString, "state:");

        int tempInt;
        if (GameData.instance.firstPlayer)
        {
            
            int.TryParse(GetDataValue(itemsDataString, "player2_energy:"),out tempInt);
            GameData.instance.enemyEnergy = tempInt;
        }
        else
        {
            int.TryParse(GetDataValue(itemsDataString, "player1_energy:"), out tempInt);
            GameData.instance.enemyEnergy = tempInt;
        }

        getData_isRuning = false;

    }

    private IEnumerator _Enqueue(string playerName)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("playername", playerName);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + enqueue, form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;
        Debug.Log(itemsDataString);

    }

    private IEnumerator _UpdateColumn(string column, string statement)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("column", column);
        form.AddField("state", statement);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + updateState, form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;
        Debug.Log(itemsDataString);

    }

    private IEnumerator LoadDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        LoadMap();
    }

    private IEnumerator GetDataDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        GetData();
    }

    private IEnumerator UpdateDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        UpdateMap();
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(",")) value = value.Remove(value.IndexOf(","));
        return value;
    }
}
