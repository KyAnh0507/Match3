using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupSpinHome : MonoBehaviour
{
    public RectTransform tf;
    public RectTransform spin;
    public List<UIRewardSpinHome> uiRewardSpinHomes;
    public RewardSpinHomeSO rewardSpinHomeSO;

    public int n;
    public GameObject noClaim;
    public GameObject claimed;

    public Transform flyAdd1;
    public Transform flyDelete;
    public Transform flyShuffle;
    public Transform flyUndo;

    public ParticleImage flyCoin;
    public ParticleImage flyGem;

    public TMP_Text textSpin;
    private void OnEnable()
    {
        spin.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        SetupSpin(DataManager.Ins.dataSaved.isClaimSpinHome);

        for (int i = 0; i < uiRewardSpinHomes.Count; i++)
        {
            uiRewardSpinHomes[i].SetupReward(rewardSpinHomeSO.datas[i]);
        }
        textSpin.text = "Spin " + DataManager.Ins.dataSaved.nSpinDaily + "/" + DataManager.Ins.dataSaved.maxSpinDaily;
    }

    public void SetupSpin(bool b)
    {
        noClaim.SetActive(!b);
        claimed.SetActive(b);


    }

    public void ClaimSpin()
    {
        if (DataManager.Ins.dataSaved.nSpinDaily > 0) DataManager.Ins.dataSaved.nSpinDaily--;
        int angle = 360*n + Random.Range(0, 360);
        UIManager.Ins.SetActiveBlock(true);
        if (DataManager.Ins.dataSaved.nSpinDaily == 0)
        {
            DataManager.Ins.dataSaved.isClaimSpinHome = true;
            SetupSpin(DataManager.Ins.dataSaved.isClaimSpinHome);
        }else
        {
            textSpin.text = "Spin " + DataManager.Ins.dataSaved.nSpinDaily + "/" + DataManager.Ins.dataSaved.maxSpinDaily;
        }
        spin.DORotate(new Vector3(0, 0, angle), 4f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ClaimReward(uiRewardSpinHomes[(int)(((angle % 360) + 22.5f) % 360) / 45]);
        });
    }

    public void ClaimReward(UIRewardSpinHome reward)
    {
        switch(reward.reward.Type)
        {
            case RewardType.Coin:
                Debug.Log("Aaaaaaaaaaaaaaaaaaaaaaaaa");
                UIManager.Ins.SetActiveBlock(true);
                flyCoin.transform.position = reward.image.transform.position;
                flyCoin.onFirstParticleFinish.AddListener(() =>
                {
                    DataManager.Ins.ChangeCoin(reward.reward.amount);
                    flyCoin.onFirstParticleFinish.RemoveAllListeners();
                });

                flyCoin.onLastParticleFinish.AddListener(() =>
                {
                    UIManager.Ins.SetActiveBlock(false);
                    flyCoin.onLastParticleFinish.RemoveAllListeners();
                });

                flyCoin.Play();
                break;
            case RewardType.Gems:
                UIManager.Ins.SetActiveBlock(true);
                flyGem.transform.position = reward.image.transform.position;
                flyGem.onFirstParticleFinish.AddListener(() =>
                {
                    DataManager.Ins.ChangeGem(reward.reward.amount);
                    flyGem.onFirstParticleFinish.RemoveAllListeners();
                });

                flyGem.onLastParticleFinish.AddListener(() =>
                {
                    UIManager.Ins.SetActiveBlock(false);
                    flyGem.onLastParticleFinish.RemoveAllListeners();
                });

                flyGem.Play();
                break;
            case RewardType.Add1Tile:
                DataManager.Ins.dataSaved.boosterAdd1 += reward.reward.amount;
                flyAdd1.gameObject.SetActive(true);
                flyAdd1.transform.localScale = Vector3.one;
                flyAdd1.position = reward.image.transform.position;
                flyAdd1.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1f).SetEase(Ease.InQuart).OnComplete(() =>
                {
                    UIManager.Ins.SetActiveBlock(false);
                    flyAdd1.gameObject.SetActive(false);
                });
                DOVirtual.DelayedCall(0.8f, () =>
                {
                    flyAdd1.DOScale(Vector3.one * 0.3f, 0.2f);
                });
                break;
            case RewardType.Undo:
                DataManager.Ins.dataSaved.boosterUndo += reward.reward.amount;
                flyUndo.gameObject.SetActive(true);
                flyUndo.transform.localScale = Vector3.one;
                flyUndo.position = reward.image.transform.position;
                flyUndo.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1f).SetEase(Ease.InQuart).OnComplete(() =>
                {
                    UIManager.Ins.SetActiveBlock(false);
                    flyUndo.gameObject.SetActive(false);
                });
                DOVirtual.DelayedCall(0.8f, () =>
                {
                    flyUndo.DOScale(Vector3.one * 0.3f, 0.2f);
                });
                break;
            case RewardType.Shuffle:
                DataManager.Ins.dataSaved.boosterSuffer += reward.reward.amount;
                flyShuffle.gameObject.SetActive(true);
                flyShuffle.transform.localScale = Vector3.one;
                flyShuffle.position = reward.image.transform.position;
                flyShuffle.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1f).SetEase(Ease.InQuart).OnComplete(() =>
                {
                    UIManager.Ins.SetActiveBlock(false);
                    flyShuffle.gameObject.SetActive(false);
                });
                DOVirtual.DelayedCall(0.8f, () =>
                {
                    flyShuffle.DOScale(Vector3.one * 0.3f, 0.2f);
                });
                break;
            case RewardType.DeleteIron:
                DataManager.Ins.dataSaved.boosterBomb += reward.reward.amount;
                flyDelete.gameObject.SetActive(true);
                flyDelete.transform.localScale = Vector3.one;
                flyDelete.position = reward.image.transform.position;
                flyDelete.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1f).SetEase(Ease.InQuart).OnComplete(() =>
                {
                    UIManager.Ins.SetActiveBlock(false);
                    flyDelete.gameObject.SetActive(false);
                });
                DOVirtual.DelayedCall(0.8f, () =>
                {
                    flyDelete.DOScale(Vector3.one * 0.3f, 0.2f);
                });
                break;
        }
        UIManager.Ins.LoadTextCoin();
    }

    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
