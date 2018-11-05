using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MatchMakingSystem : MonoBehaviour {

    private string username = "zenzai";
    private string password = "541459%server";
    private string database_IP = "http://www.brainwashgame.com";
    private string matchMaking = "/Matchmaking.php";
    private string getData = "/GetData.php";
    private string createRoom = "/CreateRoom.php";

    private bool delay;

    public InputField inputField_PlayerName;
    //public GameObject guestPanel;

    void Start()
    {
        GameData.instance.playerID = Random.seed = (int)System.DateTime.Now.Ticks;
    }

    void Update()
    {
        GameData.instance.playerName = inputField_PlayerName.text;

        if (GameData.instance.roomID != "" && !delay)
        {
            StartCoroutine(TimeDelayConnect());
        }
    }

    public void MatchMaking()
    {

        if (GameData.instance.characterPlayerName == "")
        {
            Debug.Log("Please select character.");
            return;
        }

        if (GameData.instance.playerName == "null" || GameData.instance.playerName == "npc")
        {
            Debug.Log("You can't use name 'null', Please enter new name.");
            return;
        }

        if (GameData.instance.playerName == "")
        {
            Debug.Log("Please enter name.");
            return;
        }

        //guestPanel.SetActive(false);

        Debug.Log("Player: "+GameData.instance.playerName+" Select: "+ GameData.instance.characterPlayerName);
        StartCoroutine(AskRoom(database_IP+matchMaking));
        if(GameData.instance.roomID == "")
        {
            Debug.Log("wait...");
        }
    }

    IEnumerator AskRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("playername", GameData.instance.playerName);
        form.AddField("select_character", GameData.instance.characterPlayerName);

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.Log("Connecting Error.");
            //guestPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Connecting Succeeded.");
        }

        string tempStr = www.downloadHandler.text;
        Debug.Log(tempStr);
        
        if (tempStr.Contains("Room is fully"))
        {
            GameData.instance.firstPlayer = true;
        }
        else
        {
            GameData.instance.firstPlayer = false;
        }

        addRoom(tempStr);

        if (GameData.instance.firstPlayer == true)
        {
            StartCoroutine(CreateRoom(database_IP + createRoom));
        }

    }

    void addRoom(string tempStr)
    {
        Debug.Log(tempStr);

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
        StartCoroutine(Connecting(database_IP + getData));
    }

    IEnumerator Connecting(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        string tempStr = www.downloadHandler.text;

        //Debug.Log(tempStr);

        if (GameData.instance.firstPlayer)
        {
            Debug.Log("Wait player2.");
            GameData.instance.enemyName = GetDataValue(tempStr, "player2_name:");
            GameData.instance.characterEnemyName = GetDataValue(tempStr, "player2_character:");
        }
        else
        {
            GameData.instance.enemyName = GetDataValue(tempStr, "player1_name:");
            GameData.instance.characterEnemyName = GetDataValue(tempStr, "player1_character:");
        }

    }

    IEnumerator CreateRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", GameData.instance.roomID);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error, Not create room.");
        }

        Debug.Log(www.downloadHandler.text);

    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(",")) value = value.Remove(value.IndexOf(","));
        return value;
    }
}