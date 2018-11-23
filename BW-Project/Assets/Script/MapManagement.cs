using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapManagement : MonoBehaviour
{
    public InputField inputField_roomName;
    public InputField inputField_mapSize;

    private string username = "zenzai";
    private string password = "541459%server";
    private string url = "http://www.brainwashgame.com";
    private string createRoom = "/CreateRoom.php";
    private string deleteRoom = "/DeleteRoom.php";
    private string loadMap = "/LoadMap.php";

    public GameObject[] block;
    public GameObject dummyBlock;
    private int positionX;
    private int positionZ;

    private string[] tempData;

    public bool loadMap_isRuning;

    public Transform rootMap;
    public Tile[,] map;
    public List<GameObject> allBlock;
    public int MapSize;

    public static MapManagement instance;

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
    }

    private void ResetInput()
    {
        inputField_roomName.text = "";
        inputField_mapSize.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (inputField_mapSize.text != "")
        {
            MapSize = Convert.ToInt32(inputField_mapSize.text);
        }

        if (inputField_roomName.text == "room")
        {
            inputField_roomName.text = "";
        }
    }

    public void CreateRoom()
    {
        StartCoroutine(CreateRoom(url + createRoom));
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

    public void GenerateMap()
    {
        map = new Tile[MapSize, MapSize];
        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                GenerateBlock(dummyBlock, new Vector3(positionX, 0, positionZ), i, j);
                positionZ++;
            }
            positionX++;
            positionZ = 0;
        }
    }

    public void DeleteMap()
    {
        ClearMap();

        foreach (Transform child in rootMap.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateBlock(GameObject block, Vector3 position, int row, int col)
    {
        GameObject blockTemp = Instantiate(block, rootMap.transform);
        blockTemp.GetComponent<Tile>().row = row;
        blockTemp.GetComponent<Tile>().col = col;
        blockTemp.transform.localPosition = position;
        map[row, col] = blockTemp.GetComponent<Tile>();
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

                if (tempMap[row, col] == "Building_A")
                {
                    GenerateBlock(block[0], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_B")
                {
                    GenerateBlock(block[1], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_C")
                {
                    GenerateBlock(block[2], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_D")
                {
                    GenerateBlock(block[3], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_E")
                {
                    GenerateBlock(block[4], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_F")
                {
                    GenerateBlock(block[5], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_G")
                {
                    GenerateBlock(block[6], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Building_H")
                {
                    GenerateBlock(block[7], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_A")
                {
                    GenerateBlock(block[8], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_B")
                {
                    GenerateBlock(block[9], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_C")
                {
                    GenerateBlock(block[10], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_D")
                {
                    GenerateBlock(block[11], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_Sand_A")
                {
                    GenerateBlock(block[12], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_Sand_Desert_A")
                {
                    GenerateBlock(block[13], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_Sand_Desert_B")
                {
                    GenerateBlock(block[14], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_water_A")
                {
                    GenerateBlock(block[15], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_water_B")
                {
                    GenerateBlock(block[16], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "MainFloor_water_C")
                {
                    GenerateBlock(block[17], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road")
                {
                    GenerateBlock(block[18], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road_Intersection_A")
                {
                    GenerateBlock(block[19], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road_Intersection_B")
                {
                    GenerateBlock(block[20], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road_ThirdStreed")
                {
                    GenerateBlock(block[21], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road_Turn_A")
                {
                    GenerateBlock(block[22], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road_Turn_B")
                {
                    GenerateBlock(block[23], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Rock_A")
                {
                    GenerateBlock(block[24], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Tree_A")
                {
                    GenerateBlock(block[25], new Vector3(positionX, 0, positionZ), row, col);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Tree_B")
                {
                    GenerateBlock(block[26], new Vector3(positionX, 0, positionZ), row, col);
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
        form.AddField("room_id", inputField_roomName.text);
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
            ResetInput();
        }
    }

    private IEnumerator DeleteRoom(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomName.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "")
        {
            Debug.Log("Connecting Error, Can't delete room.");
        }
        else
        {
            Debug.Log("Connecting Succeeded, " + www.downloadHandler.text);
            ResetInput();
        }
    }

    private IEnumerator GetData(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomName.text);

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
