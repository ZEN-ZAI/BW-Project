using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchMakingSystem : MonoBehaviour {

    private string username = "zenzai";
    private string password = "541459%server";
    private string database_IP = "http://www.brainwashgame.com";
    private string matchMaking = "/Matchmaking.php";
    private string getData = "/GetData.php";
    private string createRoom = "/CreateRoom.php";
    private string deleteRoom = "/DeleteRoom.php";


    private bool delay;

    void Update()
    {

        if (GameData.instance.roomID != "" && !delay)
        {
            StartCoroutine(TimeDelayConnect());
        }
    }

    public void StopMatchMaking()
    {
        GameData.instance.waitingPlayer = false;
        StopAllCoroutines();
        StartCoroutine(DeleteRoom(database_IP + deleteRoom));
    }

    public void MatchMaking()
    {

        if (GameData.instance.myCharacterName == "")
        {
            Debug.Log("Please select character.");
            return;
        }

        if (GameData.instance.myName == "null" || GameData.instance.myName == "npc")
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

        Debug.Log("Player: "+GameData.instance.myName+" Select: "+ GameData.instance.myCharacterName);

        StartCoroutine(MatchMaking(database_IP+matchMaking));
    }

    IEnumerator MatchMaking(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("playername", GameData.instance.myName);
        form.AddField("select_character", GameData.instance.myCharacterName);
        form.AddField("map_size", GameData.instance.mapSize);

        UnityWebRequest www = UnityWebRequest.Post(url,form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error.");
            GameData.instance.waitingPlayer = false;
        }
        else
        {
            Debug.Log("Connecting Succeeded, "+ www.downloadHandler.text);
        }

        string tempStr = www.downloadHandler.text;
        
        addRoom(tempStr);

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
    void addRoom(string tempStr)
    {

        if (tempStr.Contains("["))
        {
            GameData.instance.roomID = tempStr.Remove(0, tempStr.IndexOf("[") + 1);
            GameData.instance.roomID = GameData.instance.roomID.Remove(GameData.instance.roomID.Length - 1);
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

        if (GameData.instance.firstPlayer)
        {
            Debug.Log("Wait player2.");
            GameData.instance.enemyName = GetDataValue(tempStr, "player2_name:");
            GameData.instance.enemyCharacterName = GetDataValue(tempStr, "player2_character:");

        }
        else if (!GameData.instance.firstPlayer)
        {
            GameData.instance.enemyName = GetDataValue(tempStr, "player1_name:");
            GameData.instance.enemyCharacterName = GetDataValue(tempStr, "player1_character:");
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
            Debug.Log("Connecting Error.");
        }
        else
        {
            Debug.Log("Connecting Succeeded, "+ www.downloadHandler.text);
            delay = false;
            GameData.instance.roomID = "";
        }

    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(",")) value = value.Remove(value.IndexOf(","));
        return value;
    }
}