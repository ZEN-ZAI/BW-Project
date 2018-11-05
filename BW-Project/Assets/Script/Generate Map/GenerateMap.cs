using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{

    public GameObject block;

    private int positionX;
    private int positionY;
    private int positionZ;


    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                GenerateBlock(new Vector3(positionX, positionY, positionZ),i,j);
                positionZ++;
            }
            positionX++;
            positionZ = 0;
        }
    }

    void GenerateBlock(Vector3 position,int row,int col)
    {
        GameObject blockTemp = Instantiate(block, gameObject.transform);
        blockTemp.GetComponent<Tile>().row = row;
        blockTemp.GetComponent<Tile>().col = col;
        blockTemp.transform.localPosition = position;
        Map.instance.map[row, col] = blockTemp.GetComponent<Tile>();
    }

}
