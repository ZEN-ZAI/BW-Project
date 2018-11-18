using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetupGameData : MonoBehaviour
{
    public InputField inputField_PlayerName;
    //public Dropdown dropdown;
    public Toggle toggleSmall;
    public Toggle toggleMedium;
    public Toggle toggleLarge;
    public Toggle toggleWorld;
    public GameObject waitingScreen;


    public GameObject trump;
    public GameObject kim;
    public GameObject prayut;

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

        
        if (toggleSmall.isOn)
        {
            GameData.instance.mapSize = 15;
        }
        else if (toggleMedium.isOn)
        {
            GameData.instance.mapSize = 25;
        }
        else if (toggleLarge.isOn)
        {
            GameData.instance.mapSize = 35;
        }
        else if (toggleWorld.isOn)
        {
            GameData.instance.mapSize = 50;
        }
        

        if (GameData.instance.myName != "" && GameData.instance.enemyName != "")
        {

            LoadingScene.instance.LoadScene("Game");
        }
    }
}
