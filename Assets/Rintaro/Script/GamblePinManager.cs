using UnityEngine;

public class GamblePinManager : MonoBehaviour
{
    [Header("GamblePin（最初のピン）")]
    [SerializeField] private GameObject _gamblePin;

    [Header("変化後のDiceオブジェクト")]
    [SerializeField] private GameObject _dicePrefab;

    [Header("効果発動回数（デフォルト2回）")]
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
            Debug.LogError("MoneyManagerがシーンに見つかりませんでした！");
        }
      
    }

    // GamblePinの当たり判定で呼び出される
    public void TriggerEffect()
    {
        if (_effectCount >= _maxEffects) return;

        int amount = Random.value < 0.5f ? -2000 : 2000;
        Debug.Log($"Gamble効果発動: {amount}");

        if (_moneyManager != null)
        {
            _moneyManager.AddMoney(amount);
        }
        else
        {
            Debug.LogWarning("MoneyManagerがnullです！");
        }

        _effectCount++;
        Debug.Log($"効果回数: {_effectCount}");

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


