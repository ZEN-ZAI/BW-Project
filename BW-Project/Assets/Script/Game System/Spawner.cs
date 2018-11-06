using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CharacterNpc;
    public GameObject CharacterPrayut;
    public GameObject CharacterTrump;
    public GameObject CharacterKim;

    public static Spawner instance;

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

    void Spawn(GameObject character,int x,int y)
    {
        GameObject tempCharacter = Instantiate(character, Map.instance.GetBlockPosition(x,y), Quaternion.identity);
        Map.instance.map[y, x].LinkCharacter(tempCharacter);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        Map.instance.allCharacter++;
    }

    #region Random Spawn
    public void SpawnNPC()
    {
        if (Map.instance.allCharacter >= Map.instance.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int x = Random.Range(0, Map.instance.col);
        int y = Random.Range(0, Map.instance.row);

        while (Map.instance.map[y, x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            x = Random.Range(0, Map.instance.col);
            y = Random.Range(0, Map.instance.row);
        }

        Spawn(CharacterNpc, x,y);

        Debug.Log("NPC spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    public void SpawnCharacter(string playerName,string skinName)
    {
        if (Map.instance.allCharacter >= Map.instance.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int x = Random.Range(0, Map.instance.col);
        int y = Random.Range(0, Map.instance.row);

        while (Map.instance.map[y, x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            x = Random.Range(0, Map.instance.col);
            y = Random.Range(0, Map.instance.row);
        }

        //GameObject tempCharacter;

        if (skinName == "Prayut")
        {
            GameObject tempCharacter = Instantiate(CharacterPrayut, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
            Map.instance.map[y, x].LinkCharacter(tempCharacter);
            tempCharacter.GetComponent<Character>().x = x;
            tempCharacter.GetComponent<Character>().y = y;
            tempCharacter.GetComponent<Character>().group = playerName;
            Map.instance.allCharacter++;
        }
        else if (skinName == "Trump")
        {
            GameObject tempCharacter = Instantiate(CharacterTrump, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
            Map.instance.map[y, x].LinkCharacter(tempCharacter);
            tempCharacter.GetComponent<Character>().x = x;
            tempCharacter.GetComponent<Character>().y = y;
            tempCharacter.GetComponent<Character>().group = playerName;
            Map.instance.allCharacter++;
        }
        else if (skinName == "Kim")
        {
            GameObject tempCharacter = Instantiate(CharacterKim, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
            Map.instance.map[y, x].LinkCharacter(tempCharacter);
            tempCharacter.GetComponent<Character>().x = x;
            tempCharacter.GetComponent<Character>().y = y;
            tempCharacter.GetComponent<Character>().group = playerName;
            Map.instance.allCharacter++;
        }
        else
        {
            Debug.Log("Error : characterPlayerName in Spawner Player<"+ playerName + ">");
        }

        Debug.Log("Character<"+ playerName + "> spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    #endregion

    #region Position Spawn
    public void SpawnNPC(int x,int y)
    {
        Spawn(CharacterNpc, x, y);

        Debug.Log("NPC spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    public void SpawnCharacter(string playerName, string skinName, int x, int y)
    {
        GameObject tempCharacter = new GameObject();

        if (skinName == "Prayut")
        {
            tempCharacter = Instantiate(CharacterPrayut, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else if (skinName == "Trump")
        {
            tempCharacter = Instantiate(CharacterTrump, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else if (skinName == "Kim")
        {
            tempCharacter = Instantiate(CharacterKim, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else
        {
            Debug.Log("Error : characterPlayerName in Spawner");
        }

        Map.instance.map[y, x].LinkCharacter(tempCharacter);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        tempCharacter.GetComponent<Character>().group = playerName;
        Map.instance.allCharacter++;

        Debug.Log("Character<" + playerName + "> spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    #endregion

}
