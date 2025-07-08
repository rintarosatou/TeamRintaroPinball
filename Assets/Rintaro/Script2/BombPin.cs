using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPin : BankrollBase
{
    [SerializeField] private float explosionDelay = 3f; // �b���iInspector�Œ����\�j

    public override void OnBankrollEffect(GameObject ballObject)
    {
        var other = ballObject;
        if (other.transform.CompareTag("Ball"))
        {
            // ������Ball��BombBallController��t�^���Ĕ��e�ɕς���
            if (!other.gameObject.TryGetComponent(out BombBallController bombBall))
            {
                bombBall = other.gameObject.AddComponent<BombBallController>();
            }

            bombBall.StartCountdown(explosionDelay);
           // Destroy(this.gameObject); // ���e�s���͈�x�g��ꂽ�������
        }
    }
}
