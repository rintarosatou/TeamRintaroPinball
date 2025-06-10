using UnityEngine;

public class BudPinHit : MoneyGainBankrollBase
{
    public override void OnBankrollHit(GameObject ballObject)
    {
        if (ballObject.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<PinManager>().HitBudPin();
        }
    }
}
