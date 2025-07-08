using UnityEngine;

public class GamblePinHit : MoneyGainBankrollBase
{
    [SerializeField] private GamblePinManager _manager;

    public override void OnBankrollHit(GameObject ballObject)
    {
        if (ballObject.CompareTag("Ball") && _manager != null)
        {
            _manager.TriggerEffect();
        }
    }
}