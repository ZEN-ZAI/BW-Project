using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KNN : MonoBehaviour
{

    private List<NewData> newData = new List<NewData>();

    public bool KNN_finish;
    private int[] randomSet = new int[] { 2,3,4};

    public static KNN instance;

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

    public void StartKNN()
    {
        KNN_finish = false; Debug.LogWarning("KNN is Running");
        NewDataSet();
        SetCompareDataset();
    }

    public void Random_K()
    {
        UserInterfaceLink.instance.SetColorK(Color.red);
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(Delay(done =>
            {
                if (done)
                {
                    int ran = UnityEngine.Random.Range(0, randomSet.Length);
                    GameData.instance.K = randomSet[ran];
                }
            }
                ));

            if (i == 9)
            {
                StartCoroutine(Delay(done =>
                {
                    if (done)
                    {
                        UserInterfaceLink.instance.SetColorK(Color.red);
                    }
                }));
            }
        }

        UserInterfaceLink.instance.SetColorK(Color.gray);
    }

    private IEnumerator Delay(Action<bool> done)
    {
        yield return new WaitForSeconds(0.25f);
       done(true);
    }

    public void NewDataSet()
    {
        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                if (Map.instance.map[i, j].HaveCharacter())
                {
                    if (Map.instance.map[i, j].character.GetComponent<Character>().group == "Npc")
                    {
                        newData.Add(new NewData(Map.instance.map[i, j].character.GetComponent<Character>()));
                    }
                }
            }
        }
    }

    public void SetCompareDataset()
    {
        foreach (var item in newData)
        {
            item.SetCompareDataset();
            item.VoteAndNewGroup(GameData.instance.K);
        }
        KNN_finish = true;
        newData.Clear();
    }

}
public class DataSet
{
    public int distance;
    public string group;

    public DataSet(int distance, string group)
    {
        this.distance = distance;
        this.group = group;
    }
}

public class NewData
{
    public Character character;

    private List<DataSet> CompareDataset = new List<DataSet>();

    public void SetCompareDataset()
    {
        for (int i = 0; i < Map.instance.row; i++)
        {
            for (int j = 0; j < Map.instance.col; j++)
            {
                if (Map.instance.map[i, j].HaveCharacter())
                {
                    if (Map.instance.map[i, j].character.GetComponent<Character>().group == GameData.instance.myID)
                    {
                        int result = chebyshev(character.x, j, character.y, i);
                        CompareDataset.Add(new DataSet(result, GameData.instance.myID));
                    }
                    else if (Map.instance.map[i, j].character.GetComponent<Character>().group == GameData.instance.enemyID)
                    {
                        int result = chebyshev(character.x, j, character.y, i);
                        CompareDataset.Add(new DataSet(result, GameData.instance.myID));
                    }
                    else if (Map.instance.map[i, j].character.GetComponent<Character>().group == "Npc")
                    {
                        int result = chebyshev(character.x, j, character.y, i);

                        CompareDataset.Add(new DataSet(result, GameData.instance.myID));
                    } 
                }
            }
        }

        CompareDataset = CompareDataset.OrderBy(e => e.distance).ToList();
    }

    public NewData(Character character)
    {
        this.character = character;
    }

    public void VoteAndNewGroup(int k)
    {
        int tempVoteP1 = 0;
        int tempVoteP2 = 0;
        int tempVoteNpc = 0;

        for (int i = 0; i < k; i++)
        {
            if (CompareDataset[k].group == GameData.instance.myName && CompareDataset[k].distance <=5)
            {
                tempVoteP1++;
            }
            else if (CompareDataset[k].group == GameData.instance.enemyID && CompareDataset[k].distance <= 5)
            {
                tempVoteP2++;
            }
            else if (CompareDataset[k].group == "Npc" && CompareDataset[k].distance <= 5)
            {
                tempVoteNpc++;
            }
        }

        if (tempVoteP1 > tempVoteP2 && tempVoteP1 > tempVoteNpc)
        {
            character.ChangeGroup(GameData.instance.myName, GameData.instance.myCharacterName);
        }
        else if (tempVoteP2 > tempVoteP1 && tempVoteP2 > tempVoteNpc)
        {
            character.ChangeGroup(GameData.instance.enemyID, GameData.instance.enemyCharacterName);
        }
        else if (tempVoteNpc > tempVoteP1 && tempVoteNpc > tempVoteP2)
        {
            //character.ChangeGroup("Npc");
        }

    }

    int chebyshev(int herox, int monx, int heroy, int mony)
    {
        int result;
        if (Mathf.Abs(monx - herox) > Mathf.Abs(mony - heroy))
            result = Mathf.Abs(monx - herox);
        else result = Mathf.Abs(mony - heroy);

        return result;
    }
}
