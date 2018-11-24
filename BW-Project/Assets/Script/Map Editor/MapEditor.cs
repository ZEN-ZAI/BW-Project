using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapEditor : MonoBehaviour
{
    public InputField inputField_roomName;
    public InputField inputField_mapSize;

    private string username = "zenzai";
    private string password = "541459%server";
    private string url = "http://www.brainwashgame.com";
    private string createRoom = "/CreateRoom.php";
    private string deleteRoom = "/DeleteRoom.php";
    private string loadMap = "/LoadMap.php";
    private string updateMap = "/UpdateMap2.php";

    public GameObject[] block; //don't delete
    private int positionX;
    private int positionZ;

    private string[] tempData;

    public bool loadMap_isRuning;

    public Transform rootMap;
    //public GameObject[,] map;
    public string[,] tempNameBlock;
    public int MapSize;

    public static MapEditor instance;

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

    private void Start()
    {
        inputField_roomName.text = "test";
        inputField_mapSize.text = "15";
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

    public void UpdateMap()
    {
        StartCoroutine(_UpdateMap(url + updateMap));
    }

    public void DeleteMap()
    {
        tempNameBlock = null;
        positionX = 0;
        positionZ = 0;

        foreach (Transform child in rootMap.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateMap()
    {
        DeleteMap();

        tempNameBlock = new string[MapSize, MapSize];

        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                GenerateBlock(block[8], new Vector3(positionX, 0, positionZ), i, j,0);
                positionZ++;
            }
            positionX++;
            positionZ = 0;
        }
    }

    private void GenerateBlock(GameObject block, Vector3 position, int row, int col, int rotate)
    {
        GameObject blockTemp = Instantiate(block, rootMap.transform);
        blockTemp.GetComponent<Tile>().row = row;
        blockTemp.GetComponent<Tile>().col = col;
        blockTemp.transform.Rotate(new Vector3(0, rotate, 0));
        blockTemp.transform.localPosition = position;
        blockTemp.name = block.name.Replace("(Clone)", "").Trim();
        tempNameBlock[row, col] = blockTemp.name+":" +rotate;
    }

    public string tempMap;
    private IEnumerator _UpdateMap(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("room_id", inputField_roomName.text);
        form.AddField("mapSize", MapSize);

        tempMap = null;

        int num = 0;
        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                
                if (tempNameBlock[i, j] != null)
                {
                    tempMap += ""+ tempNameBlock[i, j] + "|";
                    //Debug.Log("<"+i+":"+j+ "> "+map[i, j].GetComponent<Tile>().nameBuilding + ":" + map[i, j].transform.rotation.y);
                }
                num++;
            }
        }

        form.AddField("map", tempMap);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        string itemsDataString = www.downloadHandler.text;

        if (itemsDataString == "")
        {
            Debug.Log("Connecting Error.");
        }
        else
        {
            Debug.Log("Update map.");
        }
    }

    private IEnumerator LoadMap(string url)
    {
        DeleteMap();

        tempNameBlock = new string[MapSize, MapSize];
        string[,] tempMap = new string[MapSize, MapSize];

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
        }


        tempData = itemsDataString.Split(';');
        int num = 0;

        for (int row = 0; row < MapSize; row++)
        {
            for (int col = 0; col < MapSize; col++)
            {

                tempMap[row, col] = tempData[num];

                if (tempMap[row, col].Contains("Building_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[0], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[1], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_C"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[2], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_D"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[3], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_E"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[4], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_F"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[5], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_G"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[6], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Building_H"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[7], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[8], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[9], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_C"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[10], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_D"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[11], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_Sand_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[12], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_Sand_Desert_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[13], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_Sand_Desert_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[14], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_water_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[15], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_water_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[16], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("MainFloor_water_C"))
                {
                    int temp = Convert.ToInt32(GetDataValue(tempMap[row, col], ":"));
                    GenerateBlock(block[17], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col] == "Road")
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[18], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Intersection_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[19], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Intersection_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[20], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_ThirdStreed"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[21], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Turn_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[22], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Road_Turn_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[23], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Rock_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[24], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Tree_A"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[25], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                else if (tempMap[row, col].Contains("Tree_B"))
                {
                    string str = GetDataValue(tempMap[row, col], ":");
                    int temp = Convert.ToInt32(str);
                    GenerateBlock(block[26], new Vector3(positionX, 0, positionZ), row, col, temp);
                    positionZ++;
                }
                /*else
                {
                    GenerateBlock(block[8], new Vector3(positionX, 0, positionZ), row, col, 0);
                    positionZ++;
                }*/

                num++;
            }
            positionX++;
            positionZ = 0;
        }
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
