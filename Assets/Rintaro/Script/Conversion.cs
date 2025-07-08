using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversion : MonoBehaviour
{
    public GameObject replacementPrefab; // �����ւ���v���n�u
    private int hitCount = 0; // �Փ˃J�E���g
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

        Destroy(gameObject); // ����������
        Instantiate(replacementPrefab, position, rotation); // �V�����I�u�W�F�N�g���o��
    }
}
