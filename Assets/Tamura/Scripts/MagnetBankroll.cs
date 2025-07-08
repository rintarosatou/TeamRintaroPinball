using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBankroll : BankrollBase
{
    [Header("��̔���̊p�x")]
    [SerializeField] public float _upperRange = 30f;
    [Header("���̔���̊p�x")]
    [SerializeField] public float _lowerRange = 30f;
    [Header("�E�̔���̊p�x")]
    [SerializeField] public float _rightRange = 30f;
    [Header("���̔���̊p�x")]
    [SerializeField] public float _leftRange = 30f;
    [Header("�{�[���ɉ�����͂̑傫��")]
    [SerializeField] public float _addPower = 1.0f;
    [Header("�N�[���^�C��")]
    [SerializeField] private float _intervalTime = 1f;
    private float _timer;

    private void Start()
    {
        _timer = _intervalTime;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(_timer >_intervalTime)
        {
            if (other.gameObject.tag == "Ball")
            {
                Vector3 posDelta = other.transform.position - this.transform.position;
                Rigidbody ballRb = other.attachedRigidbody;
                //�{�[������̔���ɓ��������̏���
                float upBallAngle = Vector3.Angle(this.transform.forward, posDelta);//�s���̏�͈̔�

                if (upBallAngle < _upperRange)
                {
                    Debug.Log("�{�[����_upperRange�͈͓̔��ɓ�����");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.right * -_addPower, ForceMode.Impulse);//���ɗ͂�������
                    }
                }
                //�{�[�������̔���ɓ��������̏���
                float lowBallAngle = Vector3.Angle(-this.transform.forward, posDelta);//�s���̉��͈̔�
                if (lowBallAngle < _lowerRange)

                {
                    Debug.Log("�{�[����_lowerRange�͈͓̔��ɓ�����");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.right * _addPower, ForceMode.Impulse);//�E�ɗ͂�������
                    }
                }
                //�{�[�����E�̔���ɓ��������̏���
                float rightBallAngle = Vector3.Angle(this.transform.right, posDelta);//�s���̉E�͈̔�

                if (rightBallAngle < _rightRange)
                {
                    Debug.Log("�{�[����_rightRange�͈͓̔��ɓ�����");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.forward * _addPower, ForceMode.Impulse);//��ɗ͂�������
                    }
                }
                //�{�[�������̔���ɓ��������̏���
                float leftBallAngle = Vector3.Angle(-this.transform.right, posDelta);//�s���̍��͈̔�

                if (leftBallAngle < _leftRange)
                {
                    Debug.Log("�{�[����_leftRange�͈͓̔��ɓ�����");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.forward * -_addPower, ForceMode.Impulse);//���ɗ͂�������
                    }
                }
            }

            _timer = 0;
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
