using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBallController : MonoBehaviour
{
    private float delay = 3f;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private LayerMask pinLayer;

    public void StartCountdown(float seconds)
    {
        delay = seconds;
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        Debug.Log("💥爆発！");
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            var pinEffect = hit.GetComponent<BankrollBase>();
            if (pinEffect != null)
            {
                pinEffect.OnBankrollEffect(gameObject);
            }
        }

        Destroy(this.gameObject); // 爆弾ボールは爆発後に消える
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
