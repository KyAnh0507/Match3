using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupShop : MonoBehaviour
{
    public Transform tf;
    public List<UIShopItem> uIShopItems;
    public List<UIShopItemTheme> uiShopItemsTheme;
    public List<Texture> images;
    public ParticleImage fx;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < uiShopItemsTheme.Count; i++)
        {
            uiShopItemsTheme[i].Setup();
        }

        uiShopItemsTheme[DataManager.Ins.dataSaved.theme].Buy();
    }

    public void Buy(UIShopItem uiShopItem)
    {
        bool canBuy = false;
        switch (uiShopItem.currencyType)
        {
            case RewardType.Coin:
                if (DataManager.Ins.dataSaved.coin >= uiShopItem.price)
                {
                    DataManager.Ins.ChangeCoin(-uiShopItem.price);
                    canBuy = true;
                }
                break;
            case RewardType.Gems:
                if (DataManager.Ins.dataSaved.gems >= uiShopItem.price)
                {
                    DataManager.Ins.ChangeGem(-uiShopItem.price);
                    canBuy = true;
                }
                break;
        }
        if (canBuy)
        {
            switch (uiShopItem.receiveType)
            {
                case RewardType.Add1Tile:
                    DataManager.Ins.dataSaved.boosterAdd1 += uiShopItem.quantity;
                    fx.texture = images[0];
                    fx.transform.position = uiShopItem.transform.position;
                    fx.gameObject.SetActive(true);
                    fx.Play();
                    break;
                case RewardType.DeleteIron:
                    DataManager.Ins.dataSaved.boosterBomb += uiShopItem.quantity;
                    fx.texture = images[1];
                    fx.transform.position = uiShopItem.transform.position;
                    fx.gameObject.SetActive(true);
                    fx.Play();
                    break;
                case RewardType.Shuffle:
                    DataManager.Ins.dataSaved.boosterSuffer += uiShopItem.quantity;
                    fx.texture = images[2];
                    fx.transform.position = uiShopItem.transform.position;
                    fx.gameObject.SetActive(true);
                    fx.Play();
                    break;
                case RewardType.Undo:
                    DataManager.Ins.dataSaved.boosterUndo += uiShopItem.quantity;
                    fx.texture = images[3];
                    fx.transform.position = uiShopItem.transform.position;
                    fx.gameObject.SetActive(true);
                    fx.Play();
                    break; 
            }
            UIManager.Ins.LoadTextCoin();
            if (UIManager.Ins.formGame != null)
            {
                UIManager.Ins.formGame.LoadBooster();
            }
        }

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        VibrateManager.Ins.TriggerVibrate();
    }

    public void BuyTheme(UIShopItemTheme uiShopItemTheme)
    {
        switch (uiShopItemTheme.currencyType)
        {
            case RewardType.Coin:
                if (DataManager.Ins.dataSaved.coin >= uiShopItemTheme.price)
                {
                    DataManager.Ins.ChangeCoin(-uiShopItemTheme.price);
                    uiShopItemsTheme[DataManager.Ins.dataSaved.theme].Setup();
                    DataManager.Ins.dataSaved.theme = uiShopItemTheme.nTheme;
                    DataManager.Ins.dataSaved.statusTheme[uiShopItemTheme.nTheme] = true;
                    uiShopItemTheme.Buy();
                }
                break;
            case RewardType.Gems:
                if (DataManager.Ins.dataSaved.gems >= uiShopItemTheme.price)
                {
                    DataManager.Ins.ChangeGem(-uiShopItemTheme.price);
                    uiShopItemsTheme[DataManager.Ins.dataSaved.theme].Setup();
                    DataManager.Ins.dataSaved.theme = uiShopItemTheme.nTheme;
                    DataManager.Ins.dataSaved.statusTheme[uiShopItemTheme.nTheme] = true;
                    uiShopItemTheme.Buy();
                }
                break;
        }
        UIManager.Ins.LoadBackground();

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        VibrateManager.Ins.TriggerVibrate();
    }

    public void SelectTheme(UIShopItemTheme uiShopItemTheme)
    {
        uiShopItemsTheme[DataManager.Ins.dataSaved.theme].Setup();
        DataManager.Ins.dataSaved.theme = uiShopItemTheme.nTheme;
        uiShopItemTheme.Buy();
        UIManager.Ins.LoadBackground();

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        VibrateManager.Ins.TriggerVibrate();
    }
    public void ParticalFinish()
    {
        fx.gameObject.SetActive(false);
    }

    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            if (UIManager.Ins.formGame != null)
            {
                UIManager.Ins.formGame.ResumeGame();
            }
        });
    }
}

