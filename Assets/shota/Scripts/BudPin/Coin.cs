using UnityEngine;

public class Coin : MonoBehaviour
{
    private int rewardAmount;
    private MoneyManager moneyManager;
    private float lifetime;
    private bool collected = false;

    public void Setup(MoneyManager manager, int reward, float lifeTime)
    {
        moneyManager = manager;
        rewardAmount = reward;
        lifetime = lifeTime;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Ball"))
        {
            collected = true;
            moneyManager.AddMoney(rewardAmount);
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
    private void DestroySelf()
    {
        if (!collected)
        {
            Destroy(gameObject);
        }
    }
}