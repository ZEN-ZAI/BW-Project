using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int row;
    public int col;

    public int allCharacter;
    public int maxCharacter;

    public Tile[,] map;

    public static Map instance;

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

    void Start()
    {
        row = GameData.instance.mapSize;
        col = GameData.instance.mapSize;
        map = new Tile[row, col];
        maxCharacter = row * col;
    }

    public Vector3 GetBlockPosition(int index_x, int index_y)
    {
        //return map[index_y, index_x].transform.position;
        return new Vector3(map[index_y, index_x].transform.position.x, 6, map[index_y, index_x].transform.position.z);
    }

    public void MoveCharacter(int origin_x,int origin_y, int moveto_x,int moveto_y)
    {
        map[moveto_y, moveto_x].LinkCharacter(map[origin_y, origin_x].character);
        map[origin_y, origin_x].UnLinkCharacter();
    }

    public void DestroyCharacter(int x, int y)
    {
        map[y, x].DestroyCharacter();
    }
}
