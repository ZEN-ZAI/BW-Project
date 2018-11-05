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
    public void SpawnCharacter(Player player)
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

        GameObject tempCharacter = new GameObject();

        if (player.characterPlayerName == "Prayut")
        {
            tempCharacter = Instantiate(CharacterPrayut, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else if (player.characterPlayerName == "Trump")
        {
            tempCharacter = Instantiate(CharacterTrump, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else if (player.characterPlayerName == "Kim")
        {
            tempCharacter = Instantiate(CharacterKim, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else
        {
            Debug.Log("Error : characterPlayerName in Spawner Player<"+ player.playerName+ ">");
        }

        Map.instance.map[y, x].LinkCharacter(tempCharacter);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        tempCharacter.GetComponent<Character>().group = player.playerName;
        Map.instance.allCharacter++;

        Debug.Log("Character<"+ player.playerName + "> spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    #endregion

    #region Position Spawn
    public void SpawnNPC(int x,int y)
    {
        Spawn(CharacterNpc, x, y);

        Debug.Log("NPC spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    public void SpawnCharacter(Player player,int x, int y)
    {
        GameObject tempCharacter = new GameObject();

        if (player.characterPlayerName == "Prayut")
        {
            tempCharacter = Instantiate(CharacterPrayut, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else if (player.characterPlayerName == "Trump")
        {
            tempCharacter = Instantiate(CharacterTrump, Map.instance.GetBlockPosition(x, y), Quaternion.identity);
        }
        else if (player.characterPlayerName == "Kim")
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
        tempCharacter.GetComponent<Character>().group = player.playerName;
        Map.instance.allCharacter++;

        Debug.Log("Character<" + player.playerName + "> spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    #endregion

}
