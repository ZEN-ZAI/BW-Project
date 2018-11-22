﻿using System.Collections;
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
    private string updateState = "/UpdateState.php";

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

    public void UpdateMap()
    {
        StartCoroutine(_UpdateMap());
    }

    public void GetData()
    {
        StartCoroutine(_GetData());
    }

    public void UpdateState(string state)
    {
        StartCoroutine(_UpdateState(state));
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

    private IEnumerator _LoadMap()
    {
        string[,] tempMap = new string[GameData.instance.mapSize, GameData.instance.mapSize];
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
            Debug.Log("Load map.");
            Debug.Log(itemsDataString);
        }

        tempData = itemsDataString.Split(';');
        int num = 0;

        for (int row = 0; row < Map.instance.row; row++)
        {
            for (int col = 0; col < Map.instance.col; col++)
            {

                tempMap[row, col] = tempData[num];

                if (Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Map.instance.DestroyCharacter(col, row);
                    Debug.Log("Destroy obj <x:" + col + " y:" + row + ">");
                }

                if (tempMap[row, col] == "Npc" && !Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Spawner.instance.SpawnNPC(col, row);
                    //Debug.Log("Load npc <x:" + col + " y:" + row + ">");
                }else

                if(tempMap[row, col] == GameData.instance.myID && !Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Spawner.instance.SpawnCharacter(GameData.instance.myID, GameData.instance.myCharacterName, col, row);
                    //Debug.Log("Load myPeople <x:" + col + " y:" + row + ">");
                }else

                if(tempMap[row, col] == GameData.instance.enemyID && !Map.instance.map[row, col].GetComponent<Tile>().HaveCharacter())
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

        if (GameData.instance.firstPlayer)
        {
            string temp = GetDataValue(itemsDataString, "player2_energy:");
            GameData.instance.enemyEnergy = int.Parse(temp);
        }
        else
        {
            string temp = GetDataValue(itemsDataString, "player1_energy:");
            GameData.instance.enemyEnergy = int.Parse(temp);
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

    private IEnumerator _UpdateState(string state)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("state", state);

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
