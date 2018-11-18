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

    void Start()
    {
    }

    void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {
        textKNN.text = "" + GameData.instance.K;
        textPlayerName.text = "Player: " + GameData.instance.myName;
        textEnergyPlayer.text = "" + GameData.instance.myEnergy;
        textHowManyCharacterPlayer.text = "My all people: " + GameData.instance.myAllPeople;
        textTurnPlayer.text = "" + GameData.instance.myTurn.ToString();

        textEnemyName.text = "Player: " + GameData.instance.enemyName;
        textEnergyEnemy.text = "" + GameData.instance.enemyEnergy;
        textHowManyCharacterEnemy.text = "My all people: " + GameData.instance.enemyAllPeople;
        textTurnEnemy.text = "" + GameData.instance.enemyTurn.ToString();


    }
}
