using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CharacterNpc;
    public GameObject CharacterPrayut;
    public GameObject CharacterTrump;
    public GameObject CharacterKim;

    #region Random Spawn
    public void SpawnNPC()
    {
        if (Map.instance.allCharacter >= Map.instance.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int spawn_x = Random.Range(0, Map.instance.col);
        int spawn_y = Random.Range(0, Map.instance.row);

        while (Map.instance.map[spawn_y, spawn_x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            spawn_x = Random.Range(0, Map.instance.col);
            spawn_y = Random.Range(0, Map.instance.row);
        }

        GameObject tempCharacter = Instantiate(CharacterNpc, Map.instance.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = spawn_x;
        tempCharacter.GetComponent<Character>().y = spawn_y;
        Map.instance.allCharacter++;

        Debug.Log("NPC spawn On :" + Map.instance.map[spawn_y, spawn_x].name + " <X:" + spawn_x + " Y:" + spawn_y + ">");
    }
    public void SpawnCharacter(Player player)
    {
        if (Map.instance.allCharacter >= Map.instance.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int spawn_x = Random.Range(0, Map.instance.col);
        int spawn_y = Random.Range(0, Map.instance.row);

        while (Map.instance.map[spawn_y, spawn_x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            spawn_x = Random.Range(0, Map.instance.col);
            spawn_y = Random.Range(0, Map.instance.row);
        }

        GameObject tempCharacter = new GameObject();

        if (player.characterPlayerName == "Prayut")
        {
            tempCharacter = Instantiate(CharacterPrayut, Map.instance.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        }
        else if (player.characterPlayerName == "Trump")
        {
            tempCharacter = Instantiate(CharacterTrump, Map.instance.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        }
        else if (player.characterPlayerName == "Kim")
        {
            tempCharacter = Instantiate(CharacterKim, Map.instance.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Error : characterPlayerName in Spawner Player<"+ player.playerName+ ">");
        }

        tempCharacter.GetComponent<Character>().x = spawn_x;
        tempCharacter.GetComponent<Character>().y = spawn_y;
        tempCharacter.GetComponent<Character>().group = player.playerName;
        Map.instance.allCharacter++;

        Debug.Log("Character<"+ player.playerName + "> spawn On :" + Map.instance.map[spawn_y, spawn_x].name + " <X:" + spawn_x + " Y:" + spawn_y + ">");
    }
    #endregion

    #region Position Spawn
    public void SpawnNPC(int x,int y)
    {
        GameObject tempCharacter = Instantiate(CharacterNpc, Map.instance.map[y, x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        Map.instance.allCharacter++;

        Debug.Log("NPC spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    public void SpawnCharacter(Player player,int x, int y)
    {
        GameObject tempCharacter = new GameObject();

        if (player.characterPlayerName == "Prayut")
        {
            tempCharacter = Instantiate(CharacterPrayut, Map.instance.map[y, x].transform.position, Quaternion.identity);
        }
        else if (player.characterPlayerName == "Trump")
        {
            tempCharacter = Instantiate(CharacterTrump, Map.instance.map[y, x].transform.position, Quaternion.identity);
        }
        else if (player.characterPlayerName == "Kim")
        {
            tempCharacter = Instantiate(CharacterKim, Map.instance.map[y, x].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Error : characterPlayerName in Spawner");
        }

        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        tempCharacter.GetComponent<Character>().group = player.playerName;
        Map.instance.allCharacter++;

        Debug.Log("Character<" + player.playerName + "> spawn On :" + Map.instance.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    #endregion

}
