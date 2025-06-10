using UnityEngine;

public class GamblePinManager : MonoBehaviour
{
    [Header("GamblePin�i�ŏ��̃s���j")]
    [SerializeField] private GameObject _gamblePin;

    [Header("�ω����Dice�I�u�W�F�N�g")]
    [SerializeField] private GameObject _dicePrefab;

    [Header("���ʔ����񐔁i�f�t�H���g2��j")]
    [SerializeField] private int _maxEffects = 2;

    private int _effectCount = 0;
    private MoneyManager _moneyManager;

    private int hitCount = 0;
    private Renderer pinRenderer;

    private void Start()
    {
        _moneyManager = FindObjectOfType<MoneyManager>();
        if (_moneyManager == null)
        {
            Debug.LogError("MoneyManager���V�[���Ɍ�����܂���ł����I");
        }
      
    }

    // GamblePin�̓����蔻��ŌĂяo�����
    public void TriggerEffect()
    {
        if (_effectCount >= _maxEffects) return;

        int amount = Random.value < 0.5f ? -2000 : 2000;
        Debug.Log($"Gamble���ʔ���: {amount}");

        if (_moneyManager != null)
        {
            _moneyManager.AddMoney(amount);
        }
        else
        {
            Debug.LogWarning("MoneyManager��null�ł��I");
        }

        _effectCount++;
        Debug.Log($"���ʉ�: {_effectCount}");

        if (_effectCount >= _maxEffects)
        {
            Transform pinTransform = _gamblePin.transform;
            Instantiate(_dicePrefab, pinTransform.position, pinTransform.rotation, pinTransform.parent);
            Destroy(_gamblePin);
        }
        if (hitCount >= 10)
        {
            ChangeColor();
        }
        void ChangeColor()
        {
            pinRenderer.material.color = Color.red;
        }
    }
}


