using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetupGameData : MonoBehaviour
{
    public InputField inputField_PlayerName;
    public TMP_Dropdown dropdown;
    public GameObject waitingScreen;

    // Update is called once per frame
    void Update()
    {
        GameData.instance.myName = inputField_PlayerName.text;

        if (GameData.instance.waitingPlayer)
        {
            waitingScreen.SetActive(true);
        }
        else
        {
            waitingScreen.SetActive(false);
        }

        
        if (dropdown.value == 0)
        {
            GameData.instance.mapSize = 15;
        }
        else if (dropdown.value == 1)
        {
            GameData.instance.mapSize = 25;
        }
        else if (dropdown.value == 2)
        {
            GameData.instance.mapSize = 35;
        }
        

        if (GameData.instance.myName != "" && GameData.instance.enemyName != "")
        {
            LoadingScene.instance.LoadScene("Game");
        }
    }
}
