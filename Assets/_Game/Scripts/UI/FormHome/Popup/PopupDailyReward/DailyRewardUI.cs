using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUI : MonoBehaviour
{
    public Image image;
    public Text amount;
    public GameObject block;
    public RewardData reward;
    public void SetupData(RewardData rewardData)
    {
        reward = rewardData;
        switch (rewardData.rewardType)
        {
            case RewardType.Coin:
                image.sprite = UIManager.Ins.formHome.popupDailyReward.spriteLists[0];
                break;
            case RewardType.Gems:
                image.sprite = UIManager.Ins.formHome.popupDailyReward.spriteLists[1];
                break;
        }
        amount.text = "X" + rewardData.amount;
    }
    public void Claim()
    {
        Claimed();
        UIManager.Ins.formHome.popupDailyReward.Notify(false);
        DataManager.Ins.dataSaved.isClaimDailyReward = true;
        switch (reward.rewardType)
        {
            case RewardType.Coin:
                DataManager.Ins.ChangeCoin(reward.amount);
                break;
            case RewardType.Gems:
                DataManager.Ins.ChangeGem(reward.amount);
                break;
        }
    }

    public void Claimed()
    {
        block.gameObject.SetActive(true);
    }

    public void NoClaim()
    {
        block.gameObject.SetActive(false);
    }
}

public enum RewardType
{
    Coin, Gems, CoinAndGems, Add1Tile, DeleteIron, Shuffle, Undo, TicketMasterPass
}