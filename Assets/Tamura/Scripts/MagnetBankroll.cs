using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBankroll : BankrollBase
{
    [Header("上の判定の角度")]
    [SerializeField] public float _upperRange = 30f;
    [Header("下の判定の角度")]
    [SerializeField] public float _lowerRange = 30f;
    [Header("右の判定の角度")]
    [SerializeField] public float _rightRange = 30f;
    [Header("左の判定の角度")]
    [SerializeField] public float _leftRange = 30f;
    [Header("ボールに加える力の大きさ")]
    [SerializeField] public float _addPower = 1.0f;
    [Header("クールタイム")]
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
                //ボールが上の判定に入った時の処理
                float upBallAngle = Vector3.Angle(this.transform.forward, posDelta);//ピンの上の範囲

                if (upBallAngle < _upperRange)
                {
                    Debug.Log("ボールが_upperRangeの範囲内に入った");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.right * -_addPower, ForceMode.Impulse);//左に力を加える
                    }
                }
                //ボールが下の判定に入った時の処理
                float lowBallAngle = Vector3.Angle(-this.transform.forward, posDelta);//ピンの下の範囲
                if (lowBallAngle < _lowerRange)

                {
                    Debug.Log("ボールが_lowerRangeの範囲内に入った");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.right * _addPower, ForceMode.Impulse);//右に力を加える
                    }
                }
                //ボールが右の判定に入った時の処理
                float rightBallAngle = Vector3.Angle(this.transform.right, posDelta);//ピンの右の範囲

                if (rightBallAngle < _rightRange)
                {
                    Debug.Log("ボールが_rightRangeの範囲内に入った");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.forward * _addPower, ForceMode.Impulse);//上に力を加える
                    }
                }
                //ボールが左の判定に入った時の処理
                float leftBallAngle = Vector3.Angle(-this.transform.right, posDelta);//ピンの左の範囲

                if (leftBallAngle < _leftRange)
                {
                    Debug.Log("ボールが_leftRangeの範囲内に入った");
                    if (ballRb != null)
                    {
                        ballRb.AddForce(this.transform.forward * -_addPower, ForceMode.Impulse);//下に力を加える
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
