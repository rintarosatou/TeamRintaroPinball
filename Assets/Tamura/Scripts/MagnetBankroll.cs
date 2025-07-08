using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBankroll : BankrollBase
{
    [Header("����̊p�x")]
    [SerializeField] public float _upperRange = 30f;
    [Header("�{�[���ɉ�����͂̑傫��")]
    [SerializeField] public float _addPower = 1.0f;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            Vector3 posDelta = other.transform.position - this.transform.position;
            float ballAngle = Vector3.Angle(this.transform.forward, posDelta);
            if(ballAngle < _upperRange)
            {
                Debug.Log("�{�[����_upperRange�͈͓̔��ɓ�����");
                Rigidbody ballRb = other.attachedRigidbody;
                if(ballRb != null )
                {
                    ballRb.AddForce(this.transform.right * -_addPower, ForceMode.Impulse);
                }

            }
        }
    }
    public override void OnBankrollEffect(GameObject ballObject)
    {
        //_moneyManager?.AddMoney(_addMoneyOnHit);
        //if (_ballRigidbody != null)
        //{
        //    Destroy(_ballRigidbody.gameObject);
        //}
    }
}
