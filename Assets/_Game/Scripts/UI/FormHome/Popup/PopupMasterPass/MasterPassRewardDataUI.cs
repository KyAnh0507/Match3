using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterPassRewardDataUI : MonoBehaviour
{
    public GameObject fill1;
    public GameObject fill11;
    public GameObject fill2;
    public TMP_Text textProgress;
    public GameObject complete1;
    public GameObject complete2;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject iconLock;

    public Image imageReward1;
    public Image imageReward2;
    public TMP_Text amountReward1;
    public TMP_Text amountReward2;
    public int index = -1;

    public MasterPassRewardData rewardData1;
    public MasterPassRewardData rewardData2;
    public void OnInit(MasterPassRewardData masterPassRewardData1, MasterPassRewardData masterPassRewardData2, int index)
    {
        this.index = index;
        rewardData1 = masterPassRewardData1;
        rewardData2 = masterPassRewardData2;
        textProgress.text = (index + 1) + "";
        imageReward1.sprite = masterPassRewardData1.sprite;
        imageReward2.sprite = masterPassRewardData2.sprite;
        amountReward1.text = "x" + masterPassRewardData1.rewardData.amount;
        amountReward2.text = "x" + masterPassRewardData2.rewardData.amount;
        complete1.SetActive(DataManager.Ins.dataSaved.rewardMasterPassStatus1[index]);
        complete2.SetActive(DataManager.Ins.dataSaved.rewardMasterPassStatus2[index]);

        UpdateStatusUI();
    }

    public void UpdateStatusUI()
    {
        int lv = DataManager.Ins.dataSaved.progress / 100;
        if (index == DataManager.Ins.dataSaved.maxLvMasterPass - 1)
        {
            fill1.SetActive(false);
            if (fill11 != null)
            {
                fill11.SetActive(false);
            }
            fill2.SetActive(true);

            if (index <= lv)
            {
                lock1.SetActive(false);
                lock2.SetActive(false);

            }
            else
            {
                lock1.SetActive(true);
                lock2.SetActive(true);
            }
        }
        else
        {
            if (index <= lv)
            {
                fill1.SetActive(true);
                fill2.SetActive(true);
                lock1.SetActive(false);
                lock2.SetActive(false);

            }
            else
            {
                fill1.SetActive(false);
                fill2.SetActive(false);
                lock1.SetActive(true);
                lock2.SetActive(true);
            }
        }
        if (DataManager.Ins.dataSaved.unlockedMasterPass)
        {
            iconLock.SetActive(false);
        }
        else
        {
            iconLock.SetActive(true);
            lock2.SetActive(true);
        }
    }
    public void Claim(int n)
    {
        Claimed(n);
        if (n == 0)
        {
            switch (rewardData1.rewardData.rewardType)
            {
                case RewardType.Coin:
                    DataManager.Ins.ChangeCoin(rewardData1.rewardData.amount);
                    UIManager.Ins.formHome.popupMasterPass.CollectCoinReward(complete1.transform.position);
                    break;
                case RewardType.Gems:
                    DataManager.Ins.ChangeGem(rewardData1.rewardData.amount);
                    UIManager.Ins.formHome.popupMasterPass.CollectGemReward(complete1.transform.position);
                    break;
                case RewardType.Add1Tile:
                    DataManager.Ins.dataSaved.boosterAdd1 += rewardData1.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectAdd1Reward(complete1.transform.position);
                    break;
                case RewardType.DeleteIron:
                    DataManager.Ins.dataSaved.boosterBomb += rewardData1.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectDeleteReward(complete1.transform.position);
                    break;
                case RewardType.Shuffle:
                    DataManager.Ins.dataSaved.boosterSuffer += rewardData1.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectShuffleReward(complete1.transform.position);
                    break;
                case RewardType.Undo:
                    DataManager.Ins.dataSaved.boosterUndo += rewardData1.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectUndoReward(complete1.transform.position);
                    break;
            }
        }else if (n == 1)
        {
            switch (rewardData2.rewardData.rewardType)
            {
                case RewardType.Coin:
                    DataManager.Ins.ChangeCoin(rewardData2.rewardData.amount);
                    UIManager.Ins.formHome.popupMasterPass.CollectCoinReward(complete2.transform.position);
                    break;
                case RewardType.Gems:
                    DataManager.Ins.ChangeGem(rewardData2.rewardData.amount);
                    UIManager.Ins.formHome.popupMasterPass.CollectGemReward(complete2.transform.position);
                    break;
                case RewardType.Add1Tile:
                    DataManager.Ins.dataSaved.boosterAdd1 += rewardData2.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectAdd1Reward(complete2.transform.position);
                    break;
                case RewardType.DeleteIron:
                    DataManager.Ins.dataSaved.boosterBomb += rewardData2.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectDeleteReward(complete2.transform.position);
                    break;
                case RewardType.Shuffle:
                    DataManager.Ins.dataSaved.boosterSuffer += rewardData2.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectShuffleReward(complete2.transform.position);
                    break;
                case RewardType.Undo:
                    DataManager.Ins.dataSaved.boosterUndo += rewardData2.rewardData.amount;
                    UIManager.Ins.formHome.popupMasterPass.CollectUndoReward(complete2.transform.position);
                    break;
            }
        }
        
    }

    public void Claimed(int n)
    {
        if (n == 0)
        {
            complete1.SetActive(true);
            DataManager.Ins.dataSaved.rewardMasterPassStatus1[index] = true;
        }
        else if (n == 1)
        {
            complete2.SetActive(true);
            DataManager.Ins.dataSaved.rewardMasterPassStatus2[index] = true;
        }
    }
}
