using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchMakingSystem : MonoBehaviour
{

    private string username = "zenzai";
    private string password = "541459%server";
    private string database_IP = "http://www.brainwashgame.com";
    private string matchMaking = "/Matchmaking2.php";
    private string getData = "/GetData.php";
    private string createRoom = "/CreateRoom.php";
    private string deleteRoom = "/DeleteRoom.php";

    private bool delay;

    public static MatchMakingSystem instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (GameData.instance.roomID != "" && !delay)
        {
            StartCoroutine(TimeDelayConnect());
        }

    }

    public void OpenMapEditor()
    {

        Application.OpenURL("http://www.brainwashgame.com/MapEditor/");
    }

    public void StopMatchMaking()
    {
        GameData.instance.waitingPlayer = false;
        StopAllCoroutines();
        StartCoroutine(DeleteRoom(database_IP + deleteRoom));
    }

    bool canMatch;
    public void MatchMaking()
    {

        if (SetupGameData.instance.dropdown.value == 3)
        {
            GameData.instance.waitingPlayer = true;
            StartCoroutine(CheckTable(SetupGameData.instance.inputField_MapCustomName.text, have =>
             {

                 if (have == false)
                 {
                     GameData.instance.waitingPlayer = false;
                     canMatch = false;
                     Debug.Log("Not have this map.");
                     return;
                 }
                 else if (have)
                 {
                     canMatch = true;
                     GameData.instance.mapName = SetupGameData.instance.inputField_MapCustomName.text;

                     //StartCoroutine(TestLoadElement(GameData.instance.mapName,GameData.instance.mapSize));

                     if (GameData.instance.myCharacterName == "")
                     {
                         Debug.Log("Please select character.");
                     }

                     if (GameData.instance.myName == "null" || GameData.instance.myName == "Npc")
                     {
                         Debug.Log("You can't use name 'null', Please enter new name.");
                     }

                     if (GameData.instance.myName == "")
                     {
                         Debug.Log("Please enter name.");
                     }

                     GameData.instance.waitingPlayer = true;

                     Debug.Log("Player: " + GameData.instance.myName + " Select: " + GameData.instance.myCharacterName);

                     StartCoroutine(MatchMaking(database_IP + matchMaking));
                 }


             }));
        }
        else
        {

            if (GameData.instance.myCharacterName == "")
            {
                Debug.Log("Please select character.");
                return;
            }

            if (GameData.instance.myName == "null" || GameData.instance.myName == "Npc")
            {
                Debug.Log("You can't use name 'null', Please enter new name.");
                return;
            }

            if (GameData.instance.myName == "")
            {
                Debug.Log("Please enter name.");
                return;
            }

            GameData.instance.waitingPlayer = true;

            Debug.Log("Player: " + GameData.instance.myName + " Select: " + GameData.instance.myCharacterName);

            StartCoroutine(MatchMaking(database_IP + matchMaking));

        }

    }

    IEnumerator MatchMaking(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("playerName", GameData.instance.myName);
        form.AddField("playerID", GameData.instance.myID);
        form.AddField("selectCharacter", GameData.instance.myCharacterName);

        if (SetupGameData.instance.dropdown.value == 3)
        {
            form.AddField("mapSize", GameData.instance.mapSize + ":" + GameData.instance.mapName);
        }
        else
        {
            form.AddField("mapSize", GameData.instance.mapSize);
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error.");
            GameData.instance.waitingPlayer = false;
        }
        else
        {
            Debug.Log("Connecting Succeeded, " + www.downloadHandler.text);
        }

        string tempStr = www.downloadHandler.text;

        if (tempStr.Contains("["))
        {
            GameData.instance.roomID = tempStr.Remove(0, tempStr.IndexOf("[") + 1);
            GameData.instance.roomID = GameData.instance.roomID.Remove(GameData.instance.roomID.Length - 1);
        }

        if (tempStr.Contains("Room is fully"))
        {
            GameData.instance.firstPlayer = true;
            StartCoroutine(CreateRoom(database_IP + createRoom));
        }
        else
        {
            GameData.instance.firstPlayer = false;
        }

    }

    public string[] tempData;

    private int tempSize;
    private int tempMultiple;
    IEnumerator CheckTable(string nameTable, Action<bool> have)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", nameTable);

        UnityWebRequest www = UnityWebRequest.Post(database_IP + "/LoadMap.php", form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        itemsDataString = itemsDataString.Remove(itemsDataString.Length - 1);
        Debug.Log(itemsDataString);

        tempData = itemsDataString.Split(';');

        tempSize = 0;

        if (itemsDataString == "")
        {
            Debug.Log("No have this table: " + nameTable);
            have(false);
        }
        else
        {
            Debug.Log("Have this table: " + nameTable);
            while (tempSize != tempData.Length)
            {
                tempMultiple++;
                tempSize = tempMultiple * tempMultiple;
            }

            GameData.instance.mapSize = tempMultiple;
            have(true);

        }
    }

    IEnumerator CreateRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("mapSize", GameData.instance.mapSize);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error, Not create room.");
            GameData.instance.waitingPlayer = false;
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator TimeDelayConnect()
    {
        delay = true;
        yield return new WaitForSeconds(1);
        delay = false;
        StartCoroutine(GetData(database_IP + getData));
    }

    IEnumerator GetData(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        string tempStr = www.downloadHandler.text;

        Debug.Log(tempStr);

        if (tempStr == "")
        {
            Debug.Log("Connecting Error.");
            StartCoroutine(GetData(url));
        }
        else
        {

            if (GameData.instance.firstPlayer)
            {
                Debug.Log("Wait player2.");
                GameData.instance.enemyID = GetDataValue(tempStr, "player2_ID:");
                GameData.instance.enemyName = GetDataValue(tempStr, "player2_name:");
                GameData.instance.enemyCharacterName = GetDataValue(tempStr, "player2_character:");
            }
            else if (!GameData.instance.firstPlayer)
            {
                GameData.instance.enemyID = GetDataValue(tempStr, "player1_ID:");
                GameData.instance.enemyName = GetDataValue(tempStr, "player1_name:");
                GameData.instance.enemyCharacterName = GetDataValue(tempStr, "player1_character:");
            }

            GameData.instance.state = GetDataValue(tempStr, "state:");
        }
    }

    IEnumerator DeleteRoom(string url)
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
            delay = false;
            GameData.instance.roomID = "";
        }
    }


    public void UpdateColumn(string column, string state)
    {
        StartCoroutine(_UpdateColumn(column, state));
    }

    private IEnumerator _UpdateColumn(string column, string statement)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);
        form.AddField("column", column);
        form.AddField("state", statement);

        UnityWebRequest www = UnityWebRequest.Post("http://www.brainwashgame.com/UpdateStatement.php", form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        if (www.downloadHandler.text == "")
        {
            Debug.LogError("Connecting Error, Can't update column" + column + " | " + statement);
            //StartCoroutine(_UpdateColumn(column, statement));

        }
        else
        {
            Debug.Log("Connecting Succeeded, " + www.downloadHandler.text);
        }

    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(",")) value = value.Remove(value.IndexOf(","));
        return value;
    }
}