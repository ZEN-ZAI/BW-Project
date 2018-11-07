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
    public GameObject[] rootUI;
    public TextMeshProUGUI[] textPlayerName = new TextMeshProUGUI[2];
    public TextMeshProUGUI[] textEnergyPlayer = new TextMeshProUGUI[2];
    public TextMeshProUGUI[] textHowManyCharacterPlayer = new TextMeshProUGUI[2];
    public TextMeshProUGUI[] textTurnPlayer = new TextMeshProUGUI[2];
    #endregion

    void Start()
    {
        int num = 0;
        foreach (var item in rootUI)
        {

            textPlayerName[num] = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textEnergyPlayer[num] = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            textHowManyCharacterPlayer[num] = item.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            textTurnPlayer[num] = item.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            num++;
        }
    }

    void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {
        textKNN.text = "" + GameData.instance.K;
        textPlayerName[0].text = "Player: " + GameData.instance.myName;
        textEnergyPlayer[0].text = "" + GameData.instance.myEnergy;
        textHowManyCharacterPlayer[0].text = "My all people: " + GameData.instance.myAllPeople;
        textTurnPlayer[0].text = "" + GameData.instance.myTurn.ToString();

        textPlayerName[1].text = "Player: " + GameData.instance.enemyName;
        textEnergyPlayer[1].text = "" + GameData.instance.enemyEnergy;
        textHowManyCharacterPlayer[1].text = "My all people: " + GameData.instance.enemyAllPeople;
        textTurnPlayer[1].text = "" + GameData.instance.enemyTurn.ToString();


    }
}
