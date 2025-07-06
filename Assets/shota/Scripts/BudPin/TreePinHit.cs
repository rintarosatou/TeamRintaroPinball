using UnityEngine;

public class TreePinHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<PinManager>().HitTreePin();
        }
    }
}