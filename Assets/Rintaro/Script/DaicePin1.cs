using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DaicePin1 : MonoBehaviour
{

    [Header("dicePrefab（最初のピン）")]
    [SerializeField] private GameObject _dicePrefab;

    [Header("変化後のgambleオブジェクト")]
    [SerializeField] private GameObject _gamblePin;

    [SerializeField] private GameObject pinObject;       // 消す対象のピン（このスクリプトがアタッチされているピンでも可）

    private MoneyManager moneyManager;
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトのタグを確認
        if (collision.gameObject.CompareTag("Ball"))
        {
                moneyManager = GameObject.FindAnyObjectByType<MoneyManager>();

                // 1〜6のランダムなサイコロの出目
                int diceRoll = Random.Range(1, 7);
                float multiplier = GetMultiplier(diceRoll);

                Debug.Log($"出目: {diceRoll} → 倍率: {multiplier}");

                // 現在の所持金を倍率で更新
                int originalMoney = moneyManager.Money;
                int newMoney = Mathf.RoundToInt(originalMoney * multiplier);
                int delta = newMoney - originalMoney;

                if (delta >= 0)
                {
                    moneyManager.AddMoney(delta);
                }
                else
                {
                // 減らす金額が現在の所持金を超えないようにする
                int decreaseAmount = Mathf.Min(-delta, originalMoney);
                moneyManager.DecreaseMoney(decreaseAmount);
            }

                // StartCoroutine(WaitForSecondsExample(2.0f)); // 2秒待機



                Transform pinTransform = _dicePrefab.transform;
                Instantiate(_gamblePin, pinTransform.position, pinTransform.rotation, pinTransform.parent);
                Destroy(_dicePrefab);
        }
    }
    
    //    if (other.CompareTag("Ball"))
    //    {



    //        moneyManager = GameObject.FindAnyObjectByType<MoneyManager>();

    //        // 1〜6のランダムなサイコロの出目
    //        int diceRoll = Random.Range(1, 7);
    //        float multiplier = GetMultiplier(diceRoll);

    //        Debug.Log($"出目: {diceRoll} → 倍率: {multiplier}");

    //        // 現在の所持金を倍率で更新
    //        int originalMoney = moneyManager.Money;
    //        int newMoney = Mathf.RoundToInt(originalMoney * multiplier);
    //        int delta = newMoney - originalMoney;

    //        if (delta >= 0)
    //        {
    //            moneyManager.AddMoney(delta);
    //        }
    //        else
    //        {
    //            moneyManager.DecreaseMoney(-delta);
    //        }

    //        // StartCoroutine(WaitForSecondsExample(2.0f)); // 2秒待機



    //        Transform pinTransform = _dicePrefab.transform;
    //        Instantiate(_gamblePin, pinTransform.position, pinTransform.rotation, pinTransform.parent);
    //        Destroy(_dicePrefab);
    //    }
    //}
    ////private IEnumerator WaitForSecondsExample(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);
    //    Transform pinTransform = _dicePrefab.transform;
    //    Instantiate(_gamblePin, pinTransform.position, pinTransform.rotation, pinTransform.parent);
    //    Destroy(_dicePrefab);
    //    //yield return new WaitForSeconds(seconds);
    //}

    /// <summary>
    /// 出目に応じた倍率を返す
    /// </summary>
    private float GetMultiplier(int dice)
    {
        switch (dice)
        {
            case 1: return 0.7f;
            case 2: return 0.8f;
            case 3: return 1.0f;
            case 4: return 1.2f;
            case 5: return 1.5f;
            case 6: return 2.0f;
            default: return 1.0f; // 念のため
        }
    }
}
