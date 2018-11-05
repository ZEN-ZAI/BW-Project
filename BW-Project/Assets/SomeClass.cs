using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SomeClass : MonoBehaviour
{/*
    public List<zen> playerScore = new List<zen>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            playerScore.Add(new zen(Random.Range(0, 1000), "name" + Random.Range(0, 100)));
        }

        foreach (var item in playerScore)
        {
            Debug.Log(item.name + " | " + item.score);
        }

        Debug.Log("-------------------");
        playerScore = playerScore.OrderBy(e => e.score).ToList();

        foreach (var item in playerScore)
        {
            Debug.Log(item.name +" | "+item.score);
        }
    }
    */
    /*
    private int noiseValues;
    void Start()
    {
        Random.seed = Random.seed = (int)System.DateTime.Now.Ticks;
        noiseValues = 10;
        int i = 0;
        while (i < noiseValues)
        {
            noiseValues = Random.seed;
            print(noiseValues);
            i++;
        }
    }*/


}
public class zen
{
    public int score;
    public string name;

    public zen(int score, string name)
    {
        this.score = score;
        this.name = name;
    }


}