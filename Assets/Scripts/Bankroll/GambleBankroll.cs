using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleBankroll : MonoBehaviour
{
    private int hitCount = 0; // 衝突回数
    private Renderer pinRenderer;
    // Start is called before the first frame update
    void Start()
    {
        pinRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // ボールとの衝突かどうか確認（タグで判定）
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitCount++;

            // ランダムに +2000 か -2000
            int scoreChange = Random.value < 0.5f ? 2000 : -2000;
            GameManager.Instance.AddScore(scoreChange);
            Debug.Log("Score Change: " + scoreChange);

            if (hitCount >= 10)
            {
                ChangeColor();
            }
        }
    }

    void ChangeColor()
    {
        // 任意の色に変更（例：赤）
        pinRenderer.material.color = Color.red;
    }
}
