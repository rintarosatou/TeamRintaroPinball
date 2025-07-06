using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WarpPinHolder : MonoBehaviour
{
    [SerializeField] Transform _bankrollParent;

    //�ʒu���󂯎��B
    public List<Vector3> warptrans = new List<Vector3>();
    public Vector3 warpDestination;
    private void Update()
    {
       // WarpRegister();
    }
    /// <summary>
    /// ���[�v��̓o�^
    /// </summary>

    public void WarpRegister()
    {
        //�ʒu��������
        warptrans.Clear();
        List<WarpBankroll> warpBankrollList = new List<WarpBankroll>();

        //���[�vobject��T��
        // �q�I�u�W�F�N�g��S�Ď擾����
        foreach (Transform child in _bankrollParent)
        {
            var warpBankroll = child.gameObject.GetComponent<WarpBankroll>();
            if (warpBankroll != null && warpBankroll.IsActive == true)
            {
                warpBankrollList.Add(warpBankroll);
            }
        }

        if(warpBankrollList.Count > 0)
        {
            foreach (var warpObject in warpBankrollList)�@//�ʒu��o�^
            {
                warptrans.Add(warpObject.transform.localPosition);
            }
        }
    }


    public void WarpRun(Vector3 warpStartPos)
    {
        //�ꍇ�ɕ����ă����_���Ȉʒu�����߂�B
        if (warptrans.Count >= 2)
        {
            //���Ԃ��R�s�[
            //listCopy.Remove();
            var pickPos = warptrans[Random.Range(0, warptrans.Count)];

            while (pickPos == warpStartPos)
            {
                pickPos = warptrans[Random.Range(0, warptrans.Count)];
            }
            warpDestination = pickPos;
        }
        else
        {
            //�����̈ʒu������
            Vector3 warpDestination = warpStartPos;
        }
    }

}
