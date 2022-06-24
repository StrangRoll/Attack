using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _moneyText;

    private void OnEnable()
    {
        _player.MoneyChaged += ChangeMoneyText;
        _moneyText.text = _player.Money.ToString();
    }

    private void OnDisable()
    {
        _player.MoneyChaged -= ChangeMoneyText;
    }

    private void ChangeMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }
}
