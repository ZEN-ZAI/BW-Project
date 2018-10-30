using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Map Map;

    public GameObject CharacterNPC;
    public GameObject CharacterMyCharacter;
    public GameObject CharacterEnermy;

    #region Random Spawn
    public void SpawnNPC()
    {
        if (Map.allCharacter >= Map.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int spawn_x = Random.Range(0, Map.col);
        int spawn_y = Random.Range(0, Map.row);

        while (Map.map[spawn_y, spawn_x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            spawn_x = Random.Range(0, Map.col);
            spawn_y = Random.Range(0, Map.row);
        }

        GameObject tempCharacter = Instantiate(GameData.instance.npc, Map.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = spawn_x;
        tempCharacter.GetComponent<Character>().y = spawn_y;
        Map.allCharacter++;

        Debug.Log("NPC spawn On :" + Map.map[spawn_y, spawn_x].name + " <X:" + spawn_x + " Y:" + spawn_y + ">");
    }
    public void SpawnMyCharacter()
    {
        if (Map.allCharacter >= Map.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int spawn_x = Random.Range(0, Map.col);
        int spawn_y = Random.Range(0, Map.row);

        while (Map.map[spawn_y, spawn_x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            spawn_x = Random.Range(0, Map.col);
            spawn_y = Random.Range(0, Map.row);
        }

        GameObject tempCharacter = Instantiate(GameData.instance.characterPlayer, Map.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = spawn_x;
        tempCharacter.GetComponent<Character>().y = spawn_y;
        tempCharacter.GetComponent<Character>().group = GameData.instance.playerName;
        Map.allCharacter++;

        Debug.Log("My Character spawn On :" + Map.map[spawn_y, spawn_x].name + " <X:" + spawn_x + " Y:" + spawn_y + ">");
    }
    public void SpawnEnemy()
    {
        if (Map.allCharacter >= Map.maxCharacter)
        {
            Debug.Log("Error: Map is full");
            return;
        }

        int spawn_x = Random.Range(0, Map.col);
        int spawn_y = Random.Range(0, Map.row);

        while (Map.map[spawn_y, spawn_x].GetComponent<Tile>().HaveCharacter())
        {
            Debug.Log("Error: Spawn Repeated");
            spawn_x = Random.Range(0, Map.col);
            spawn_y = Random.Range(0, Map.row);
        }

        GameObject tempCharacter = Instantiate(GameData.instance.characterEnemy, Map.map[spawn_y, spawn_x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = spawn_x;
        tempCharacter.GetComponent<Character>().y = spawn_y;
        tempCharacter.GetComponent<Character>().group = GameData.instance.enemyName;
        Map.allCharacter++;

        Debug.Log("Character Enemy spawn On :" + Map.map[spawn_y, spawn_x].name + " <X:" + spawn_x + " Y:" + spawn_y + ">");
    }
    #endregion

    #region Position Spawn
    public void SpawnNPC(int x,int y)
    {
        GameObject tempCharacter = Instantiate(GameData.instance.npc, Map.map[y, x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        Map.allCharacter++;

        Debug.Log("NPC spawn On :" + Map.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    public void SpawnMyCharacter(int x, int y)
    {
        GameObject tempCharacter = Instantiate(GameData.instance.characterPlayer, Map.map[y, x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        tempCharacter.GetComponent<Character>().group = GameData.instance.playerName;
        Map.allCharacter++;

        Debug.Log("Character Player spawn On :" + Map.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    public void SpawnEnemy(int x, int y)
    {
        GameObject tempCharacter = Instantiate(GameData.instance.characterEnemy, Map.map[y, x].transform.position, Quaternion.identity);
        tempCharacter.GetComponent<Character>().x = x;
        tempCharacter.GetComponent<Character>().y = y;
        tempCharacter.GetComponent<Character>().group = GameData.instance.enemyName;
        Map.allCharacter++;

        Debug.Log("Character Enemy spawn On :" + Map.map[y, x].name + " <X:" + x + " Y:" + y + ">");
    }
    #endregion

}
