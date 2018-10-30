using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceLink : MonoBehaviour
{
    #region System Attribute
    public Text textTime;
    #endregion

    #region Player Attribute
    public Text textPlayerName;
    public Text textEnergyPlayer;
    public Text textHowManyCharacterPlayer;
    public Text textTurnPlayer;
    #endregion

    #region Enemy Attribute
    public Text textEnemyName;
    public Text textEnergyEnemy;
    public Text textHowManyCharacterEnemy;
    public Text textTurnEnemy;
    #endregion

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {

    }

    private void UIUpdatePlayer()
    {
        /*textPlayerName = GameSystem.instance;
        textEnergyPlayer;
        textHowManyCharacterPlayer;
        textTurnPlayer;*/
    }

    private void UIUpdateEnemy()
    {

    }
}
