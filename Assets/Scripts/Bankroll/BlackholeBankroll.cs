using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeBankroll : BankrollBase
{
    [Header("�z���̋���")]
    [SerializeField] private float _inhalePower = 10f;
    [Header("�{�[���������������ɑ����邨��")]
    [SerializeField] private int _addMoneyOnHit = 200;
    //[Header("���ʔ͈�")]
    //[SerializeField] private float _effectRadius = 5f;

    private MoneyManager _moneyManager;
    private List<Rigidbody> _ballsInRange = new List<Rigidbody>();

    void Start()
    {
        _moneyManager = GameObject.FindAnyObjectByType<MoneyManager>();
        StartCoroutine(InhaleBallOnRange());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody ballRb = other.attachedRigidbody;
            if (ballRb != null && !_ballsInRange.Contains(ballRb))
            {
                _ballsInRange.Add(ballRb);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody ballRb = other.attachedRigidbody;
            if (ballRb != null)
            {
                _ballsInRange.Remove(ballRb);
            }
        }
    }

    /// <summary>
    /// ���ʔ͈͓��̃{�[�����o���N���[���̕����ɋz���񂹂�
    /// </summary>
    private IEnumerator InhaleBallOnRange()
    {
        while (true)
        {
            foreach (var ball in _ballsInRange)
            {
                if (ball != null)
                {
                    Vector3 direction = (transform.position - ball.position).normalized;
                    ball.AddForce(direction * _inhalePower, ForceMode.Acceleration);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void OnBankrollEffect(GameObject ballObject)
    {
        _moneyManager?.AddMoney(_addMoneyOnHit);
        if (_ballRigidbody != null)
        {
            Destroy(_ballRigidbody.gameObject);
        }
    }
}
