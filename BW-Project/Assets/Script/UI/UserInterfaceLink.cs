using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterfaceLink : MonoBehaviour
{
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
    }
}
