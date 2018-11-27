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

    public GameObject panelEND;
    public TextMeshProUGUI textEND;

    #region System Attribute
    public TextMeshProUGUI textKNN;
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

    public static UserInterfaceLink instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {
        if (GameData.instance.state == "END")
        {
            panelEND.SetActive(true);

            if (GameSystem.instance.playerWin == GameData.instance.myID)
            {
                textEND.text = "YOU WIN";
            }
            else if (GameSystem.instance.playerWin == GameData.instance.enemyID)
            {
                textEND.text = "YOU LOSE";
            }
            else
            {
                textEND.text = "-DRAW-";
            }

        }

        textKNN.text = "" + GameData.instance.K;
        textPlayerName.text = "" + GameData.instance.myName;
        textEnergyPlayer.text = "" + GameData.instance.myEnergy;
        textHowManyCharacterPlayer.text = "My people: " + GameData.instance.myAllPeople;
        textTurnPlayer.text = "" + GameData.instance.myTurn.ToString();

        if (GameData.instance.myTurn)
        {
            textTurnPlayer.text = "Your Turn.";
            textTurnEnemy.text = "Enemy Waiting";
        }
        else
        {
            textTurnPlayer.text = "Please Wait.";
            textTurnEnemy.text = "Enemy Playing";
        }

        textEnemyName.text = "" + GameData.instance.enemyName;
        textEnergyEnemy.text = "" + GameData.instance.enemyEnergy;
        textHowManyCharacterEnemy.text = "My people: " + GameData.instance.enemyAllPeople;

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

    public void SetColorK(Color color)
    {
        textKNN.color = color;
    }
}
