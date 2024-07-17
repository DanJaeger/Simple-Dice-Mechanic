using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _diceOneText;
    [SerializeField] private TMP_Text _diceTwoText;//In case we use more than one dice

    private void OnEnable()
    {
        Dice.OnDiceResult += SetText;
    }
    private void SetText(int diceIndex, int diceResult)
    {
        if(diceIndex == 0)
        {
            _diceOneText.SetText($"DICE ONE ROLLE A {diceResult}");
        }
        //else
        //{
        //    _diceTwoText.SetText($"DICE TWO ROLLE A {diceResult}"); //In case we use more than one dice
        //}
    }
    private void OnDisable()
    {
        Dice.OnDiceResult -= SetText;
    }
}
