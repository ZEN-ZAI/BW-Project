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
        textKNN.text = ""+KNN.instance.K;
        UpdatePlayer(0);
        UpdatePlayer(1);
    }

    private void UpdatePlayer(int index)
    {
        textPlayerName[index].text = "Player 1: "+GameSystem.instance.player[index].playerName;
        textEnergyPlayer[index].text = ""+GameSystem.instance.player[index].energy;
        textHowManyCharacterPlayer[index].text = "My all people: "+GameSystem.instance.player[index].myAllPeople;
        textTurnPlayer[index].text = ""+GameSystem.instance.player[index].myTurn.ToString();
    }
}
