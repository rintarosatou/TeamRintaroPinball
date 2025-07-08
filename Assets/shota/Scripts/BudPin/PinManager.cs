using UnityEngine;

public class PinManager : MonoBehaviour
{
    [Header("ピンの設定")]
    [SerializeField] private GameObject budPin;
    [SerializeField] private GameObject treePin;

    [Header("ヒット条件設定")]
    [SerializeField] private int budPinHitsPerReward = 5;
    [SerializeField] private int budPinRewardTimesToSwitch = 2;
    [SerializeField] private int treePinHitThreshold = 5;

    [Header("マネーマネージャー")]
    [SerializeField] private MoneyManager moneyManager;

    [Header("コイン設定")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinSpawnCount = 10;
    [SerializeField] private float coinLifetime = 10f;
    [SerializeField] private float coinScatterRadius = 3f;

    private int budPinHitCount = 0;
    private int budPinRewardCount = 0;
    private int treePinHitCount = 0;

    private void Start()
    {
        budPin.SetActive(true);
        treePin.SetActive(false);
    }

    public void HitBudPin()
    {
        budPinHitCount++;
        Debug.Log($"BudPin hit: {budPinHitCount}/{budPinHitsPerReward}");

        if (budPinHitCount >= budPinHitsPerReward)
        {
            moneyManager.AddMoney(1000);
            budPinRewardCount++;
            budPinHitCount = 0; // リセット

            Debug.Log($"BudPin reward {budPinRewardCount}/{budPinRewardTimesToSwitch}");

            if (budPinRewardCount >= budPinRewardTimesToSwitch)
            {
                SwitchToTreePin();
            }
        }
    }

    public void HitTreePin()
    {
        treePinHitCount++;
        Debug.Log($"TreePin hit: {treePinHitCount}/{treePinHitThreshold}");

        if (treePinHitCount >= treePinHitThreshold)
        {
            moneyManager.AddMoney(25000);
            SpawnCoins();
            SwitchToBudPin();
        }
    }

    private void SwitchToTreePin()
    {
        budPin.SetActive(false);
        treePin.SetActive(true);
        budPinHitCount = 0;
        budPinRewardCount = 0;
        treePinHitCount = 0;
        Debug.Log("Switched to TreePin!");
    }

    private void SwitchToBudPin()
    {
        treePin.SetActive(false);
        budPin.SetActive(true);
        budPinHitCount = 0;
        budPinRewardCount = 0;
        treePinHitCount = 0;
        Debug.Log("Switched back to BudPin!");
    }
    private void SpawnCoins()
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("コインプレハブが設定されていません！");
            return;
        }

        for (int i = 0; i < coinSpawnCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-coinScatterRadius, coinScatterRadius),
                0f,
                Random.Range(-coinScatterRadius, coinScatterRadius)
                );

            Vector3 spawnPos = treePin.transform.position + randomOffset;
            spawnPos.y += 1.1f;

            GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            Coin coinScript = coin.GetComponent<Coin>();
            if (coinScript != null)
            {
                coinScript.Setup(moneyManager, 500, coinLifetime);
            }
        }
    }
}