using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversion : MonoBehaviour
{
    public GameObject replacementPrefab; // 差し替え先プレハブ
    private int hitCount = 0; // 衝突カウント
                              // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        hitCount++;

        Debug.Log("Hit Count: " + hitCount);

        if (hitCount >= 10)
        {
            ReplaceObject();
        }
    }

    void ReplaceObject()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        Destroy(gameObject); // 自分を消す
        Instantiate(replacementPrefab, position, rotation); // 新しいオブジェクトを出す
    }
}
