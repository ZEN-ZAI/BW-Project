using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSystem : MonoBehaviour {

    private string username = "zenzai";
    private string password = "541459%server";
    private string database_IP = "http://www.brainwashgame.com";
    private string getData = "/GetData.php";
    private string updateMap = "/UpdateMap.php";
    private string loadMap = "/LoadMap.php";

    private string[] tempData;

    public bool loadMap_isRuning;
    public bool updateMap_isRuning;

    public IEnumerator LoadMap(Map map)
    {
        string[,] tempMap = new string[map.row, map.col];
        loadMap_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + loadMap, form);
        yield return www.downloadHandler.text;

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

        for (int row = 0; row < map.row; row++)
        {
            for (int col = 0; col < map.col; col++)
            {
                tempMap[row, col] = tempData[num];

                if (tempMap[row, col] == "" && map.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    map.DestroyCharacter(col, row);
                    Debug.Log("Destroy obj <x:" + col + " y:" + row + ">");
                }
                if (tempMap[row, col] == "npc" && map.map[row, col].GetComponent<Tile>().HaveCharacter())
                {
                    Spawner.instance.SpawnNPC(col, row);
                    Debug.Log("Load npc <x:" + col + " y:" + row + ">");
                }
                if (tempMap[row, col] == GameData.instance.myName)
                {
                    Spawner.instance.SpawnCharacter(GameData.instance.myName, GameData.instance.myCharacterName, col, row);
                    Debug.Log("Load myPeople <x:" + col + " y:" + row + ">");
                }
                if (tempMap[row, col] == GameData.instance.enemyName)
                {
                    Spawner.instance.SpawnCharacter(GameData.instance.enemyName, GameData.instance.enemyCharacterName, col, row);
                    Debug.Log("Load enemy <x:" + col + " y:" + row + ">");
                }
                num++;
            }
        }

        loadMap_isRuning = false;
    }

     public IEnumerator UpdateMap(Map map)
    {
        updateMap_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        int num = 0;
        for (int i = 0; i < map.row; i++)
        {
            for (int j = 0; j < map.col; j++)
            {
                if (map.map[i, j].GetComponent<Tile>().HaveCharacter())
                {
                    form.AddField("map" + num, map.map[i, j].GetComponent<Tile>().character.GetComponent<Character>().group);
                }
                else
                {
                    form.AddField("map" + num, "");
                }
                num++;
            }
        }

        UnityWebRequest www = UnityWebRequest.Post(database_IP + updateMap ,form);
        yield return www.downloadHandler.text;

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

    IEnumerator Connecting()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        WWW dataReturn = new WWW(database_IP+ getData, form);
        yield return dataReturn;

        string itemsDataString = dataReturn.text;
        Debug.Log(itemsDataString);

        //GameData.instance.enemyName = GetDataValue(itemsDataString, "player1_name:");
        //GameData.instance.enemyCharacter = GetDataValue(itemsDataString, "player1_character:");

    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(",")) value = value.Remove(value.IndexOf(","));
        return value;
    }
}
