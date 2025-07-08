using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WarpBankroll : BankrollBase
{
    //[Header("吸引の強さ")]
    //[SerializeField] private float _inhalePower = 10f;
    //[Header("ボールのプレハブ")]
    //[SerializeField] private GameObject _ballPrefab;
    [Header("射出する強さ")]
    [SerializeField] private float _shootPower = 1f;
    //[Header("クールタイム")]
    //[SerializeField] private float _intervalTime = 10f;
    [Header("ボールがワープするまでの待機時間")]
    [SerializeField] private float _warpDelay = 0.5f;
    //[Header("効果範囲")]
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
    /// 効果範囲内のボールをバンクロールの方向に吸い寄せる
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
        Debug.Log("ボールがあたった");
        //if (_timer > _intervalTime)
        //{
            _warpPinHolder.WarpRun(transform.position);
            //WarpPinHolderからワープ先の位置を受け取る。
            Vector3 destinationPosition = _warpPinHolder.warpDestination;
            Transform pinTransform = transform;
            // ピンの前方にずらす
            destinationPosition -= pinTransform.forward * 0.5f;
            RaycastHit hit;
            Vector3 rayOrigin = destinationPosition + Vector3.up * 5f; // 上からRayを飛ばす

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 10f))
            {
                if (hit.collider.gameObject.name == "Table") // ← オブジェクト名が "Table" のときだけ
                {
                    destinationPosition.y = hit.point.y + 0.3f;
                }
            }
            //destinationPosition.z += -1.5f;
            //ボールとワープピンの当たり判定を無くす
            Collider ballCollider = ballObject.GetComponent<Collider>();
            // シーン上のすべての WarpPin を取得（WarpPin スクリプトがアタッチされている前提）
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
                // ボールとピンの位置が近すぎる場合は復活させない
                float distance = Vector3.Distance(col1.transform.position, col2.transform.position);
                if (distance > 2f) // 0.5f は許容距離。必要に応じて調整
                {
                    Physics.IgnoreCollision(col1, col2, false);
                }
                else
                {
                    Debug.Log("ボールがピンと重なっているため、当たり判定を復活させませんでした");
                }
            }
            // ボールを指定された位置で生成
            StartCoroutine(WarpBallAfterDelay(ballObject, destinationPosition));
            ////ボール生成時にカウントする
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
        //ボールをワープするまで姿だけ非表示する
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.enabled = false;
        }

        yield return new WaitForSeconds(_warpDelay); // 指定された秒数待つ
        ball.transform.position = targetPosition;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 shootDirection = -transform.forward + Vector3.up * 0.2f;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = shootDirection.normalized * _shootPower;
        }
        //ボールを再表示する
        if (ballRenderer != null)
        {
            ballRenderer.enabled = true;
        }
    }

    public void OnMouseDown()
    {
        
    }
}
