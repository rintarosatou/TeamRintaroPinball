using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WarpBankroll : BankrollBase
{
    //[Header("�z���̋���")]
    //[SerializeField] private float _inhalePower = 10f;
    //[Header("�{�[���̃v���n�u")]
    //[SerializeField] private GameObject _ballPrefab;
    [Header("�ˏo���鋭��")]
    [SerializeField] private float _shootPower = 1f;
    //[Header("�N�[���^�C��")]
    //[SerializeField] private float _intervalTime = 10f;
    [Header("�{�[�������[�v����܂ł̑ҋ@����")]
    [SerializeField] private float _warpDelay = 0.5f;
    //[Header("���ʔ͈�")]
    //[SerializeField] private float _effectRadius = 5f;
    private BallManager _ballManager;
    private WarpPinHolder _warpPinHolder;
    private List<Rigidbody> _ballsInRange = new List<Rigidbody>();
    public bool IsActive;

    private void Awake()
    {
        IsActive = true;
    }
    void Start()
    {
        _ballManager = GameObject.FindObjectOfType<BallManager>();
        //_timer = _intervalTime;
        //StartCoroutine(InhaleBallOnRange());
        _warpPinHolder = FindAnyObjectByType<WarpPinHolder>();
    }

private void Update()
    {
        //_timer += Time.deltaTime;
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
    //private IEnumerator InhaleBallOnRange()
    //{
    //    while (true)
    //    {
    //        foreach (var ball in _ballsInRange)
    //        {
    //            if (ball != null)
    //            {
    //                Vector3 direction = (transform.position - ball.position).normalized;
    //                ball.AddForce(direction * _inhalePower, ForceMode.Acceleration);
    //            }
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    public override void OnBankrollEffect(GameObject ballObject) 
    {
        Debug.Log("�{�[������������");
        //if (_timer > _intervalTime)
        //{
            _warpPinHolder.WarpRun(transform.position);
            //WarpPinHolder���烏�[�v��̈ʒu���󂯎��B
            Vector3 destinationPosition = _warpPinHolder.warpDestination;
            Transform pinTransform = transform;
            // �s���̑O���ɂ��炷
            destinationPosition -= pinTransform.forward * 0.5f;
            RaycastHit hit;
            Vector3 rayOrigin = destinationPosition + Vector3.up * 5f; // �ォ��Ray���΂�

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 10f))
            {
                if (hit.collider.gameObject.name == "Table") // �� �I�u�W�F�N�g���� "Table" �̂Ƃ�����
                {
                    destinationPosition.y = hit.point.y + 0.3f;
                }
            }
            //destinationPosition.z += -1.5f;
            //�{�[���ƃ��[�v�s���̓����蔻��𖳂���
            Collider ballCollider = ballObject.GetComponent<Collider>();
            // �V�[����̂��ׂĂ� WarpPin ���擾�iWarpPin �X�N���v�g���A�^�b�`����Ă���O��j
            WarpBankroll[] allPins = FindObjectsOfType<WarpBankroll>();

            foreach (WarpBankroll pin in allPins)
            {
                Collider pinCollider = pin.GetComponent<Collider>();
                if (pinCollider != null && ballCollider != null)
                {
                    Physics.IgnoreCollision(pinCollider, ballCollider, true);
                    StartCoroutine(RestoreCollisionLater(pinCollider, ballCollider));
                }
            }

            IEnumerator RestoreCollisionLater(Collider col1, Collider col2)
            {
                yield return new WaitForSeconds(1f);
                // �{�[���ƃs���̈ʒu���߂�����ꍇ�͕��������Ȃ�
                float distance = Vector3.Distance(col1.transform.position, col2.transform.position);
                if (distance > 2f) // 0.5f �͋��e�����B�K�v�ɉ����Ē���
                {
                    Physics.IgnoreCollision(col1, col2, false);
                }
                else
                {
                    Debug.Log("�{�[�����s���Əd�Ȃ��Ă��邽�߁A�����蔻��𕜊������܂���ł���");
                }
            }
            // �{�[�����w�肳�ꂽ�ʒu�Ő���
            StartCoroutine(WarpBallAfterDelay(ballObject, destinationPosition));
            ////�{�[���������ɃJ�E���g����
            //_ballManager.AddBallCount();
            //_timer = 0;

            //if (_ballRigidbody != null)
            //{
            //    Destroy(_ballRigidbody.gameObject);
            //}
        //}
    }
    private IEnumerator WarpBallAfterDelay(GameObject ball, Vector3 targetPosition)
    {
        //�{�[�������[�v����܂Ŏp������\������
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.enabled = false;
        }

        yield return new WaitForSeconds(_warpDelay); // �w�肳�ꂽ�b���҂�
        ball.transform.position = targetPosition;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 shootDirection = -transform.forward + Vector3.up * 0.2f;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = shootDirection.normalized * _shootPower;
        }
        //�{�[�����ĕ\������
        if (ballRenderer != null)
        {
            ballRenderer.enabled = true;
        }
    }

    public void OnMouseDown()
    {
        
    }
}
