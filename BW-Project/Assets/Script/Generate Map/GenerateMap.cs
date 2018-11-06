using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{

    public GameObject block;

    private int positionX;
    //private int positionY;
    private int positionZ;

    public static GenerateMap instance;

    public bool End;

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

    public void Generate()
    {
        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                GenerateBlock(new Vector3(positionX, 0, positionZ),i,j);
                positionZ++;
            }
            positionX++;
            positionZ = 0;
        }
    }

    private void GenerateBlock(Vector3 position,int row,int col)
    {
        GameObject blockTemp = Instantiate(block, gameObject.transform);
        blockTemp.GetComponent<Tile>().row = row;
        blockTemp.GetComponent<Tile>().col = col;
        blockTemp.transform.localPosition = position;
        Map.instance.map[row, col] = blockTemp.GetComponent<Tile>();
    }

}
