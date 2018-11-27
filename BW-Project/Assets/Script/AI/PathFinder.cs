using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public string position;
    public string parent;

    public Transform transformPosition;

    public Node(string position, string parent, Transform tempTranform)
    {
        this.position = position;
        this.parent = parent;
        transformPosition = tempTranform;
    }
}

public class PathFinder : MonoBehaviour
{

    private Map Map;

    public int maxLevelFind;
    /*
    private int c_topLevelfind = 0;
    private int c_underLevelfind = 0;
    private int c_leftLevelfind = 0;
    private int c_rightLevelfind = 0;

    private int c_TLLevelfind = 0;
    private int c_TRLevelfind = 0;
    private int c_ULLevelfind = 0;
    private int c_URLevelfind = 0;
    */
    private int bfsLevel = 1;
    private int degreeToNextLevel = 0;
    private bool firstLoopPass;

    private Queue<Transform> q = new Queue<Transform>();
    private List<Transform> listQ = new List<Transform>();

    public List<List<Transform>> paths = new List<List<Transform>>();

    void Start()
    {
        //Debug.Log("Test -> "+Map.instance.map[0,0].transform.name);
    }

    void Update()
    {
        //maxLevelFind = GameData.instance.energy;
    }

    public void PathFinding(int index_x, int index_y,int energy)
    {
        //DepthFirstSearch(index_y, index_x);
        maxLevelFind = energy;
        BreadthFirstSearch(index_x, index_y);
    }

    #region wait for connect
    /*
    public Stack<Transform> stack = new Stack<Transform>();
    public List<Node> upTree = new List<Node>();
    public Transform tempParent;
    public bool findGoal;

    public void DFS(int x, int y)
    {
        upTree.Add(new Node(Map.instance.map[y, x].name,"-", Map.instance.map[y, x].transform));
        tempParent = Map.instance.map[y, x].transform;
        while (!findGoal)
        {
            if (y < Map.instance.row && x < Map.instance.col && y >= 0 && x >= 0)
            {



                BackTack(x, y - 1); //Top
                BackTack(x + 1, y - 1); //Top Right
                BackTack(x + 1, y); //Right
                BackTack(x + 1, y + 1); //Under Right
                BackTack(x, y + 1); //Under
                BackTack(x - 1, y + 1); //Under Left
                BackTack(x - 1, y);    //Left
                BackTack(x - 1, y - 1); //Top Left

                Transform tempTranform = stack.Pop();
                upTree.Add(new Node(tempTranform.name, tempParent.name, tempTranform));
                tempParent = tempTranform;

            }
        }
    }

    public void BackTack(int x, int y)
    {
        if (y < Map.instance.row && x < Map.instance.col && y >= 0 && x >= 0)
        {
            stack.Push(Map.instance.map[y, x].transform);
        }
    }            /*int tempindex;
            tempindex = upTree.FindIndex(e => e.position == Map.instance.map[y, x].name);

            if (upTree[tempindex].parent == null)
            {
                upTree[tempindex].parent = tempParent;
            }*/
    /*
    BackTack(x, y - 1); //Top
    BackTack(x + 1, y - 1); //Top Right
    BackTack(x + 1, y); //Right
    BackTack(x + 1, y + 1); //Under Right
    BackTack(x, y + 1); //Under
    BackTack(x - 1, y + 1); //Under Left
    BackTack(x - 1, y);    //Left
    BackTack(x - 1, y - 1); //Top Left
    */
    #endregion

    #region Breadth first search

    void BreadthFirstSearch(int index_x, int index_y)
    {
        if (q.Count == 0)
        {
            listQ.Add(Map.instance.map[index_y, index_x].transform);
            q.Enqueue(Map.instance.map[index_y, index_x].transform);
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
        if (Map.instance.map[index_y, index_x].HaveCharacter())
        {
            Map.instance.map[index_y, index_x].pathLevel = 0; //AddlevelPath
        }
        else
        {
            Map.instance.map[index_y, index_x].pathLevel = bfsLevel; //AddlevelPath

            //Map.instance.map[index_y, index_x].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
            Map.instance.map[index_y, index_x].GetComponent<SetMaterial>().Highlight();
        }
    }

    bool Visited(int index_x, int index_y)
    {
        return Map.instance.map[index_y, index_x].visited;
    }
    void SetVisited(int index_x, int index_y)
    {
        Map.instance.map[index_y, index_x].visited = true;
    }
    void CheckAndAddPath(int index_x, int index_y)
    {
        if (index_y < Map.instance.row && index_x < Map.instance.col && index_y >= 0 && index_x >= 0)
        {

            if (!Visited(index_x, index_y))
            {
                listQ.Add(Map.instance.map[index_y, index_x].transform);
                SetVisited(index_x, index_y);

                HighlightPath(index_x, index_y);

                q.Enqueue(Map.instance.map[index_y, index_x].transform);
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
            item.GetComponent<SetMaterial>().UnHighlight();
        }
        listQ.Clear();
        Debug.Log("ResetPathBFS");
    }
    #endregion

    #region Depth frist search
    /*
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
                ele.GetComponent<SetMaterial>().SetDefaultMaterial();
                ele.GetComponent<Tile>().pathLevel = 0;
            }
        }
    }

    void TopPath(int index_y, int index_x)
    {if (Map.instance.map[index_y - 1, index_x] != null && c_topLevelfind < maxLevelFind)
            {
                paths[0].Add(Map.instance.map[index_y - 1, index_x].GetComponent<Transform>());
                Map.instance.map[index_y - 1, index_x].GetComponent<Tile>().pathLevel = c_topLevelfind + 1;
                Map.instance.map[index_y - 1, index_x].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
            Debug.Log("Open path[Level " + maxLevelFind + "]: top");

                c_topLevelfind++;
                TopPath((index_y - 1), index_x);
            }
        //top
        try
        {
            
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
            if (Map.instance.map[index_y, index_x + 1] != null && c_rightLevelfind < maxLevelFind)
            {
                paths[1].Add(Map.instance.map[index_y, index_x + 1].GetComponent<Transform>());
                Map.instance.map[index_y, index_x + 1].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
            if (Map.instance.map[index_y + 1, index_x] != null && c_underLevelfind < maxLevelFind)
            {
                paths[2].Add(Map.instance.map[index_y + 1, index_x].GetComponent<Transform>());
                Map.instance.map[index_y + 1, index_x].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
            if (Map.instance.map[index_y, index_x - 1] != null && c_leftLevelfind < maxLevelFind)
            {
                paths[3].Add(Map.instance.map[index_y, index_x - 1].GetComponent<Transform>());
                Map.instance.map[index_y, index_x - 1].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
            if (Map.instance.map[index_y + 1, index_x + 1] != null && c_URLevelfind < maxLevelFind)
            {
                Map.instance.map[index_y + 1, index_x + 1].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
            if (Map.instance.map[index_y + 1, index_x - 1] != null && c_ULLevelfind < maxLevelFind)
            {
                Map.instance.map[index_y + 1, index_x - 1].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
            if (Map.instance.map[index_y - 1, index_x + 1] != null && c_TRLevelfind < maxLevelFind)
            {
                Map.instance.map[index_y - 1, index_x + 1].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
            if (Map.instance.map[index_y - 1, index_x - 1] != null && c_TLLevelfind < maxLevelFind)
            {
                Map.instance.map[index_y - 1, index_x - 1].GetComponent<Renderer>().material.shader = aPathMaterial.shader;
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
    */
    #endregion
    
}