using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapManagement : MonoBehaviour
{
    public InputField inputField_roomNumber;
    public InputField inputField_roomName;
    public InputField inputField_mapSize;

    private string username = "zenzai";
    private string password = "541459%server";
    private string url = "http://www.brainwashgame.com";
    private string createRoom = "/CreateRoom.php";
    private string deleteRoom = "/DeleteRoom.php";
    private string loadMap = "/LoadMap.php";

    public GameObject block;
    private int positionX;
    private int positionZ;

    private string[] tempData;

    public bool loadMap_isRuning;

    public Tile[,] map;
    public List<GameObject> allBlock;
    public int MapSize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inputField_mapSize.text != "")
        {
            MapSize = Convert.ToInt32(inputField_mapSize.text);
        }
    }

    public void CreateRoom()
    {
        StartCoroutine(CreateRoom(url+createRoom));
    }


    public void DeleteRoom()
    {
        StartCoroutine(DeleteRoom(url + deleteRoom));
    }

    public void LoadMap()
    {
        StartCoroutine(LoadMap(url + loadMap));
    }

    private void ClearMap()
    {
        map = null;
        positionX = 0;
        positionZ = 0;
        allBlock.Clear();
    }


    private void Generate()
    {
        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                GenerateBlock(block,new Vector3(positionX, 0, positionZ), i, j);
                positionZ++;
            }
            positionX++;
            positionZ = 0;
        }
    }

    private void GenerateBlock(GameObject block,Vector3 position, int row, int col)
    {
        GameObject blockTemp = Instantiate(block, gameObject.transform);
        blockTemp.GetComponent<Tile>().row = row;
        blockTemp.GetComponent<Tile>().col = col;
        blockTemp.transform.localPosition = position;
        map[row, col] = blockTemp.GetComponent<Tile>();
        allBlock.Add(blockTemp);
    }

    private IEnumerator LoadMap(string url)
    {
        ClearMap();
        map = new Tile[MapSize, MapSize];

        string[,] tempMap = new string[MapSize, MapSize];
        loadMap_isRuning = true;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomName.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
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

        for (int row = 0; row < MapSize; row++)
        {
            for (int col = 0; col < MapSize; col++)
            {

                tempMap[row, col] = tempData[num];

                if (tempMap[row, col] == "")
                {
                    GenerateBlock(block, new Vector3(positionX, 0, positionZ),row,col);
                    positionZ++;
                }
                
                num++;
            }
            positionX++;
            positionZ = 0;
        }

        loadMap_isRuning = false;
    }

    private IEnumerator CreateRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomNumber.text);
        form.AddField("mapSize", inputField_mapSize.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error, Can't create room.");
        }
        else
        {
            Debug.Log("Connecting Succeeded, " + www.downloadHandler.text);
        }
    }

    private IEnumerator DeleteRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomNumber.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error, Can't delete room.");
        }
        else
        {
            Debug.Log("Connecting Succeeded, " + www.downloadHandler.text);
        }
    }

    private IEnumerator GetData(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomNumber.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

    }

    private string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(",")) value = value.Remove(value.IndexOf(","));
        return value;
    }
}
