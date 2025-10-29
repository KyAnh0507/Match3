using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWinstreak : MonoBehaviour
{
    public RectTransform tf;
    public List<WinstreakRewardUI> winstreakRewardUIs = new List<WinstreakRewardUI>();

    public WinstreakRewardDataSO rewardDataSO;
    public List<Sprite> spriteLists = new List<Sprite>();

    public ParticleImage flyCoin;
    public ParticleImage flyGem;

    public GameObject buttonClaim;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < winstreakRewardUIs.Count; i++)
        {
            winstreakRewardUIs[i].SetupData(rewardDataSO.rewardDatas[i]);
        }
        flyCoin.gameObject.SetActive(false);
        flyGem.gameObject.SetActive(false);
    }


    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
