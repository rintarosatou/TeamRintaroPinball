using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPin : BankrollBase
{
    [SerializeField] private float explosionDelay = 3f; // 秒数（Inspectorで調整可能）

    public override void OnBankrollEffect(GameObject ballObject)
    {
        var other = ballObject;
        if (other.transform.CompareTag("Ball"))
        {
            // 既存のBallにBombBallControllerを付与して爆弾に変える
            if (!other.gameObject.TryGetComponent(out BombBallController bombBall))
            {
                bombBall = other.gameObject.AddComponent<BombBallController>();
            }

            bombBall.StartCountdown(explosionDelay);
           // Destroy(this.gameObject); // 爆弾ピンは一度使われたら消える
        }
    }
}
