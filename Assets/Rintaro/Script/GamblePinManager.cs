using System.Collections;
using UnityEngine;

public class GamblePinManager : MoneyGainBankrollBase
{
    [Header("GamblePin�i�ŏ��̃s���j")]
    [SerializeField] private GameObject _gamblePin;

    [Header("�ω����Dice�I�u�W�F�N�g")]
    [SerializeField] private GameObject _dicePrefab;

    [Header("���ʔ����񐔁i�f�t�H���g2��j")]
    [SerializeField] private int _maxEffects = 2;

    private int _effectCount = 0;
    private MoneyManager _moneyManager;
    private void Start()
    {
        _moneyManager = FindObjectOfType<MoneyManager>();
        if (_moneyManager == null)
        {
            Debug.LogError("MoneyManager���V�[���Ɍ�����܂���ł����I");
        }

    }
    public override void OnBankrollHit(GameObject ballObject)
    {
        TriggerEffect();
    }

    // GamblePin�̓����蔻��ŌĂяo�����
    public void TriggerEffect()
    {
        if (_effectCount >= _maxEffects) return;

        int amount = Random.value < 0.5f ? -2000 : 2000;
        Debug.Log($"Gamble���ʔ���: {amount}");
        if (_moneyManager != null)
        {
            int currentMoney = _moneyManager.Money;
            if (amount >= 0)
            {
                _moneyManager.AddMoney(amount);
            }
            else
            {
                // ���炷���z�����݂̏������𒴂��Ȃ��悤�ɒ���
                int decreaseAmount = Mathf.Min(-amount, currentMoney);
                _moneyManager.DecreaseMoney(decreaseAmount);
            }
        }
        else
        {
            Debug.LogWarning("MoneyManager��null�ł��I");
        }
        _effectCount++;
        Debug.Log($"���ʉ�: {_effectCount}");

        StartCoroutine(WaitForSecondsExample(1.0f)); // 1�b�ҋ@

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


