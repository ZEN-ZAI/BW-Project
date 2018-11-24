using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] block;

    public int positionX;
    //private int positionY;
    public int positionZ;

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

    public void GenerateDefalut()
    {
        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                GenerateBlock(8,new Vector3(positionX, 0, positionZ),i,j,0);
                positionZ++;
            }
            positionX++;
            positionZ = 0;
        }
    }

    public void GenerateBlock(int indexBlock,Vector3 position,int row,int col,int rotate)
    {
        GameObject blockTemp = Instantiate(block[indexBlock], gameObject.transform);
        blockTemp.GetComponent<Tile>().row = row;
        blockTemp.GetComponent<Tile>().col = col;
        blockTemp.transform.Rotate(new Vector3(0, rotate, 0));
        blockTemp.transform.localPosition = position;
        Map.instance.map[row, col] = blockTemp.GetComponent<Tile>();
    }

}
