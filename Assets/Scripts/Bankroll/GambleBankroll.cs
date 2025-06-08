using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleBankroll : MonoBehaviour
{
    private int hitCount = 0; // �Փˉ�
    private Renderer pinRenderer;
    // Start is called before the first frame update
    void Start()
    {
        pinRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // �{�[���Ƃ̏Փ˂��ǂ����m�F�i�^�O�Ŕ���j
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitCount++;

            // �����_���� +2000 �� -2000
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
        // �C�ӂ̐F�ɕύX�i��F�ԁj
        pinRenderer.material.color = Color.red;
    }
}
