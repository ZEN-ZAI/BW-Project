using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Material[] aPathMaterial; // level search

    private Map aTableControl;

    public int maxLevelFind;

    private int c_topLevelfind = 0;
    private int c_underLevelfind = 0;
    private int c_leftLevelfind = 0;
    private int c_rightLevelfind = 0;

    private int c_TLLevelfind = 0;
    private int c_TRLevelfind = 0;
    private int c_ULLevelfind = 0;
    private int c_URLevelfind = 0;

    private int bfsLevel = 1;
    private int degreeToNextLevel = 0;
    private bool firstLoopPass;

    private Queue<Transform> q = new Queue<Transform>();
    private List<Transform> listQ = new List<Transform>();

    public List<List<Transform>> paths = new List<List<Transform>>();

    void Start()
    {
        aTableControl = FindObjectOfType<Map>();

    }

    void Update()
    {
        maxLevelFind = GameData.instance.energy;
    }

    public void PathFinding(int index_x, int index_y)
    {
        //DepthFirstSearch(index_y, index_x);
        BreadthFirstSearch(index_x, index_y);
    }

    #region Breadth first search

    void BreadthFirstSearch(int index_x, int index_y)
    {
        if (q.Count == 0)
        {
            listQ.Add(aTableControl.map[index_y, index_x].GetComponent<Transform>());
            q.Enqueue(aTableControl.map[index_y, index_x].GetComponent<Transform>());
            SetVisited(index_x, index_y);
        }
        BFS();
    }
    void BFS()
    {
        while (q.Count != 0)
        {

            Transform temp = q.Dequeue();
            int tempX = temp.GetComponent<Tile>().col;
            int tempY = temp.GetComponent<Tile>().row;

            if (firstLoopPass)
            {
                degreeToNextLevel--;
            }

            if (bfsLevel <= maxLevelFind)
            {
                AddChild(tempX, tempY);
            }

            if (degreeToNextLevel == 0)
            {
                bfsLevel++;
                degreeToNextLevel = q.Count;
            }
            if (!firstLoopPass)
            {
                firstLoopPass = true;
            }

        }
    }
    void AddChild(int index_x, int index_y)
    {
        CheckAndAddPath(index_x, index_y - 1); //Top
        CheckAndAddPath(index_x + 1, index_y - 1); //Top Right
        CheckAndAddPath(index_x + 1, index_y); //Right
        CheckAndAddPath(index_x + 1, index_y + 1); //Under Right
        CheckAndAddPath(index_x, index_y + 1); //Under
        CheckAndAddPath(index_x - 1, index_y + 1); //Under Left
        CheckAndAddPath(index_x - 1, index_y);    //Left
        CheckAndAddPath(index_x - 1, index_y - 1); //Top Left

    }
    void HighlightPath(int index_x, int index_y)
    {
        if (aTableControl.map[index_y, index_x].GetComponent<Tile>().HaveCharacter())
        {
            aTableControl.map[index_y, index_x].GetComponent<Tile>().pathLevel = 0; //AddlevelPath
        }
        else
        {
            aTableControl.map[index_y, index_x].GetComponent<Tile>().pathLevel = bfsLevel; //AddlevelPath
            aTableControl.map[index_y, index_x].GetComponent<Renderer>().sharedMaterial = aPathMaterial[bfsLevel - 1];
        }
    }

    bool Visited(int index_x, int index_y)
    {
        return aTableControl.map[index_y, index_x].GetComponent<Tile>().visited;
    }
    void SetVisited(int index_x, int index_y)
    {
        aTableControl.map[index_y, index_x].GetComponent<Tile>().visited = true;
    }
    void CheckAndAddPath(int index_x, int index_y)
    {
        if (index_y < aTableControl.row && index_x < aTableControl.col && index_y >= 0 && index_x >= 0)
        {

            if (!Visited(index_x, index_y))
            {
                listQ.Add(aTableControl.map[index_y, index_x].GetComponent<Transform>());
                SetVisited(index_x, index_y);

                HighlightPath(index_x, index_y);

                q.Enqueue(aTableControl.map[index_y, index_x].GetComponent<Transform>());
            }


        }
    }
    public void ResetPathBFS()
    {
        bfsLevel = 1;
        degreeToNextLevel = 0;
        firstLoopPass = false;
        foreach (var item in listQ)
        {
            item.GetComponent<Tile>().pathLevel = 0;
            item.GetComponent<Tile>().visited = false;
            item.GetComponent<DefaultMaterial>().SetDefaultMaterial();
        }
        listQ.Clear();
        Debug.Log("ResetPathBFS");
    }
    #endregion
    #region Depth frist search

    void DepthFirstSearch(int index_y, int index_x)
    {
        paths.Add(new List<Transform>());
        TopPath(index_y, index_x);
        paths.Add(new List<Transform>());
        TRPath(index_y, index_x);
        paths.Add(new List<Transform>());
        RightPath(index_y, index_x);
        paths.Add(new List<Transform>());
        URPath(index_y, index_x);
        paths.Add(new List<Transform>());
        UnderPath(index_y, index_x);
        paths.Add(new List<Transform>());
        ULPath(index_y, index_x);
        paths.Add(new List<Transform>());
        LeftPath(index_y, index_x);
        paths.Add(new List<Transform>());
        TLPath(index_y, index_x);
    }
    public void ResetPathDFS()
    {
        ResetC_Levelfind();
        ResetHighlight();
        paths.Clear();
    }
    void ResetC_Levelfind()
    {
        c_topLevelfind = 0;
        c_underLevelfind = 0;
        c_leftLevelfind = 0;
        c_rightLevelfind = 0;

        c_TLLevelfind = 0;
        c_TRLevelfind = 0;
        c_ULLevelfind = 0;
        c_URLevelfind = 0;
    }
    void ResetHighlight()
    {
        foreach (var item in paths)
        {
            foreach (var ele in item)
            {
                ele.GetComponent<DefaultMaterial>().SetDefaultMaterial();
                ele.GetComponent<Tile>().pathLevel = 0;
            }
        }
    }

    void TopPath(int index_y, int index_x)
    {
        //top
        try
        {
            if (aTableControl.map[index_y - 1, index_x] != null && c_topLevelfind < maxLevelFind)
            {
                paths[0].Add(aTableControl.map[index_y - 1, index_x].GetComponent<Transform>());
                aTableControl.map[index_y - 1, index_x].GetComponent<Tile>().pathLevel = c_topLevelfind + 1;
                aTableControl.map[index_y - 1, index_x].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_topLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: top");

                c_topLevelfind++;
                TopPath((index_y - 1), index_x);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array -> top");
        }
    }
    void RightPath(int index_y, int index_x)
    {
        //right
        try
        {
            if (aTableControl.map[index_y, index_x + 1] != null && c_rightLevelfind < maxLevelFind)
            {
                paths[1].Add(aTableControl.map[index_y, index_x + 1].GetComponent<Transform>());
                aTableControl.map[index_y, index_x + 1].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_rightLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: right");
                c_rightLevelfind++;
                RightPath(index_y, (index_x + 1));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    void UnderPath(int index_y, int index_x)
    {
        //under
        try
        {
            if (aTableControl.map[index_y + 1, index_x] != null && c_underLevelfind < maxLevelFind)
            {
                paths[2].Add(aTableControl.map[index_y + 1, index_x].GetComponent<Transform>());
                aTableControl.map[index_y + 1, index_x].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_underLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: under");

                c_underLevelfind++;
                UnderPath((index_y + 1), index_x);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    void LeftPath(int index_y, int index_x)
    {
        //left
        try
        {
            if (aTableControl.map[index_y, index_x - 1] != null && c_leftLevelfind < maxLevelFind)
            {
                paths[3].Add(aTableControl.map[index_y, index_x - 1].GetComponent<Transform>());
                aTableControl.map[index_y, index_x - 1].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_leftLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: left");
                c_leftLevelfind++;
                LeftPath(index_y, (index_x - 1));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    void URPath(int index_y, int index_x)
    {
        //under & right
        try
        {
            if (aTableControl.map[index_y + 1, index_x + 1] != null && c_URLevelfind < maxLevelFind)
            {
                aTableControl.map[index_y + 1, index_x + 1].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_URLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: under & right");
                c_URLevelfind++;
                URPath((index_y + 1), (index_x + 1));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    void ULPath(int index_y, int index_x)
    {
        //under & left
        try
        {
            if (aTableControl.map[index_y + 1, index_x - 1] != null && c_ULLevelfind < maxLevelFind)
            {
                aTableControl.map[index_y + 1, index_x - 1].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_ULLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: under & left");
                c_ULLevelfind++;
                ULPath((index_y + 1), (index_x - 1));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    void TRPath(int index_y, int index_x)
    {
        //top & right
        try
        {
            if (aTableControl.map[index_y - 1, index_x + 1] != null && c_TRLevelfind < maxLevelFind)
            {
                aTableControl.map[index_y - 1, index_x + 1].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_TRLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: top & right");
                c_TRLevelfind++;
                TRPath((index_y - 1), (index_x + 1));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    void TLPath(int index_y, int index_x)
    {
        //top & left
        try
        {
            if (aTableControl.map[index_y - 1, index_x - 1] != null && c_TLLevelfind < maxLevelFind)
            {
                aTableControl.map[index_y - 1, index_x - 1].GetComponent<Renderer>().sharedMaterial = aPathMaterial[c_TLLevelfind];
                Debug.Log("Open path[Level " + maxLevelFind + "]: top & left");
                c_TLLevelfind++;
                TLPath((index_y - 1), (index_x - 1));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Out of Array");
        }
    }
    #endregion
}