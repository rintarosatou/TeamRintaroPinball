using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WarpPinHolder : MonoBehaviour
{
    [SerializeField] Transform _bankrollParent;

    //位置を受け取る。
    public List<Vector3> warptrans = new List<Vector3>();
    public Vector3 warpDestination;
    private void Update()
    {
       // WarpRegister();
    }
    /// <summary>
    /// ワープ先の登録
    /// </summary>

    public void WarpRegister()
    {
        //位置を初期化
        warptrans.Clear();
        List<WarpBankroll> warpBankrollList = new List<WarpBankroll>();

        //ワープobjectを探す
        // 子オブジェクトを全て取得する
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
            foreach (var warpObject in warpBankrollList)　//位置を登録
            {
                warptrans.Add(warpObject.transform.localPosition);
            }
        }
    }


    public void WarpRun(Vector3 warpStartPos)
    {
        //場合に分けてランダムな位置を決める。
        if (warptrans.Count >= 2)
        {
            //実態をコピー
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
            //自分の位置を入れる
            Vector3 warpDestination = warpStartPos;
        }
    }

}
