using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupShop : MonoBehaviour
{
    public Transform tf;
    public List<UIShopItem> uIShopItems;
    public List<Texture> images;
    public ParticleImage fx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Buy(UIShopItem uiShopItem)
    {
        bool canBuy = false;
        switch (uiShopItem.currencyType)
        {
            case RewardType.Coin:
                if (DataManager.Ins.dataSaved.coin >= uiShopItem.price)
                {
                    DataManager.Ins.dataSaved.coin -= uiShopItem.price;
                    canBuy = true;
                }
                break;
            case RewardType.Gems:
                if (DataManager.Ins.dataSaved.gems >= uiShopItem.price)
                {
                    DataManager.Ins.dataSaved.gems -= uiShopItem.price;
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
        }
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
        });
    }
}

