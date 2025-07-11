using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPin : MonoBehaviour
{
    [Header("もう一方のピン")]
    [SerializeField] private Collider otherPinCollider;

    [Header("自分のCollider")]
    [SerializeField] private Collider myCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            myCollider.enabled = false;
            otherPinCollider.enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
