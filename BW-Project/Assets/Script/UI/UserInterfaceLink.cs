using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceLink : MonoBehaviour
{
    public Image statusPlayer1;
    public Image statusPlayer2;

    public Sprite playing;
    public Sprite waiting;

    #region System Attribute
    public TextMeshProUGUI textKNN;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textEnd;
    #endregion

    #region Player Attribute
    public TextMeshProUGUI textPlayerName;
    public TextMeshProUGUI textEnergyPlayer;
    public TextMeshProUGUI textHowManyCharacterPlayer;
    public TextMeshProUGUI textTurnPlayer;
    #endregion

    #region Enemy Attribute
    public TextMeshProUGUI textEnemyName;
    public TextMeshProUGUI textEnergyEnemy;
    public TextMeshProUGUI textHowManyCharacterEnemy;
    public TextMeshProUGUI textTurnEnemy;
    #endregion

    void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {
        textKNN.text = "" + GameData.instance.K;
        textPlayerName.text = "" + GameData.instance.myName;
        textEnergyPlayer.text = "" + GameData.instance.myEnergy;
        textHowManyCharacterPlayer.text = "My people: " + GameData.instance.myAllPeople;
        textTurnPlayer.text = "" + GameData.instance.myTurn.ToString();

        textTurnPlayer.text = "" + GameData.instance.myTurn.ToString();
        textTurnEnemy.text = "" + GameData.instance.enemyTurn.ToString();

        textEnemyName.text = "" + GameData.instance.enemyName;
        textEnergyEnemy.text = "" + GameData.instance.enemyEnergy;
        textHowManyCharacterEnemy.text = "My people: " + GameData.instance.enemyAllPeople;

        textEnd.text = "" + GameSystem.instance.playerWin;

        if (GameData.instance.myTurn)
        {
            statusPlayer1.sprite = playing;
        }
        else
        {
            statusPlayer1.sprite = waiting;
        }


        if (GameData.instance.enemyTurn)
        {
            statusPlayer2.sprite = playing;
        }
        else
        {
            statusPlayer2.sprite = waiting;
        }
    }
}
