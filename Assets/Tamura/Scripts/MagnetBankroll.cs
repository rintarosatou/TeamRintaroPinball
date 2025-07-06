using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBankroll : BankrollBase
{
    private MoneyManager _moneyManager;

    void Start()
    {
        _moneyManager = GameObject.FindAnyObjectByType<MoneyManager>();
    }

    public override void OnBankrollEffect(GameObject ballObject)
    {

    }
}
