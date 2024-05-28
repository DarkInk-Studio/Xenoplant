using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public Money GlobalMoney;

    public TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.text = $"{GlobalMoney.money} $";
    }
}
