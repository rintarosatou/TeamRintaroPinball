using System.Collections;
using UnityEngine;

public class GamblePinManager : MoneyGainBankrollBase
{
    [Header("GamblePin（最初のピン）")]
    [SerializeField] private GameObject _gamblePin;

    [Header("変化後のDiceオブジェクト")]
    [SerializeField] private GameObject _dicePrefab;

    [Header("効果発動回数（デフォルト2回）")]
    [SerializeField] private int _maxEffects = 2;

    private int _effectCount = 0;
    private MoneyManager _moneyManager;
    private void Start()
    {
        _moneyManager = FindObjectOfType<MoneyManager>();
        if (_moneyManager == null)
        {
            Debug.LogError("MoneyManagerがシーンに見つかりませんでした！");
        }

    }
    public override void OnBankrollHit(GameObject ballObject)
    {
        TriggerEffect();
    }

    // GamblePinの当たり判定で呼び出される
    public void TriggerEffect()
    {
        if (_effectCount >= _maxEffects) return;

        int amount = Random.value < 0.5f ? -2000 : 2000;
        Debug.Log($"Gamble効果発動: {amount}");
        if (_moneyManager != null)
        {
            int currentMoney = _moneyManager.Money;
            if (amount >= 0)
            {
                _moneyManager.AddMoney(amount);
            }
            else
            {
                // 減らす金額が現在の所持金を超えないように調整
                int decreaseAmount = Mathf.Min(-amount, currentMoney);
                _moneyManager.DecreaseMoney(decreaseAmount);
            }
        }
        else
        {
            Debug.LogWarning("MoneyManagerがnullです！");
        }
        _effectCount++;
        Debug.Log($"効果回数: {_effectCount}");

        StartCoroutine(WaitForSecondsExample(1.0f)); // 1秒待機

    }
    private IEnumerator WaitForSecondsExample(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (_effectCount >= _maxEffects)
        {
            Transform pinTransform = _gamblePin.transform;
            Instantiate(_dicePrefab, pinTransform.position, pinTransform.rotation, pinTransform.parent);
            Destroy(_gamblePin);
        }

        yield return new WaitForSeconds(seconds);
    }

}


