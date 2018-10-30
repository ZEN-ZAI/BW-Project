using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCharacter : MonoBehaviour
{
    public GameObject trump;
    public GameObject kim;
    public GameObject prayut;

    // Update is called once per frame
    void Update()
    {
        if (GameData.instance.playerName != "" && GameData.instance.enemyName != "")
        {
            if (GameData.instance.characterPlayerName == "Prayut")
            {
                GameData.instance.characterPlayer = prayut;
            }
            else if (GameData.instance.characterPlayerName == "Kim")
            {
                GameData.instance.characterPlayer = kim;
            }
            else if (GameData.instance.characterPlayerName == "Trump")
            {
                GameData.instance.characterPlayer = trump;
            }

            if (GameData.instance.characterEnemyName == "Prayut")
            {
                GameData.instance.characterEnemy = prayut;
            }
            else if (GameData.instance.characterEnemyName == "Kim")
            {
                GameData.instance.characterEnemy = kim;
            }
            else if (GameData.instance.characterEnemyName == "Trump")
            {
                GameData.instance.characterEnemy = trump;
            }

            GameData.instance.npc.GetComponent<Character>().group = "npc";
            GameData.instance.characterPlayer.GetComponent<Character>().group = GameData.instance.playerName;
            GameData.instance.characterEnemy.GetComponent<Character>().group = GameData.instance.enemyName;

            LoadingScene.instance.LoadScene("Game");
        }
    }
}
