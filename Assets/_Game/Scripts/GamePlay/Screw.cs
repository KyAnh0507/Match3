using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : GameUnit
{
    public int layer;
    public int screwType;
    public Transform imageScrewPins;
    public Transform imageScrew;
    public Vector3 positionButton;
    public bool canPlay = true;
    public Collider2D collider2D;
    public SpriteRenderer spriteScrew;
    public SpriteRenderer spriteScrewPins;
    public void OnInit(int layer)
    {
        this.layer = layer;
        ChangeLayer(layer);
    }
    
    public void Move(Vector3 pos)
    {
        canPlay = false;
        PullUp(pos);

    }

    public void Move1(Vector3 pos)
    {
        TF.DOKill();
        TF.DOMove(pos, 0.2f).OnComplete(() =>
        {
            PullDown();
        });
    }
    public void PullUp(Vector3 pos)
    {
        imageScrew.DOLocalMove(positionButton + new Vector3(0, 0.3f, 0), 0.15f);
        ChangeSortingLayer();
        imageScrewPins.DOLocalMove(new Vector3(0, 0.2f, 0), 0.15f).OnComplete(() =>
        {
            TF.DOMove(pos, 0.12f).OnComplete(() =>
            {
                PullDown();
            });
        });
    }

    public void PullDown()
    {
        DOVirtual.DelayedCall(0.08f, () =>
        {
            imageScrew.DOLocalMove(positionButton, 0.15f);
            imageScrewPins.DOLocalMove(new Vector3(0, 0.13f, 0), 0.15f);
        });
    }

    public void Match3()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            TF.DOLocalMoveY(TF.position.y + 0.2f, 0.5f);
            TF.DORotate(Vector3.back * 180, 0.5f).OnComplete(() =>
            {
                Despawn();
            });
        });
        
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void ChangeScrewType(int screwType)
    {
        spriteScrew.color = GamePlay.Ins.color[screwType];
        spriteScrewPins.color = GamePlay.Ins.color[screwType];
        this.screwType = screwType;
    }
    public void ChangeLayer(int layer)
    {
        gameObject.layer = layer + 5;
        spriteScrew.sortingOrder = layer * 10 + 2;
        spriteScrewPins.sortingOrder = layer * 10 + 1;
    }

    public void ChangeSortingLayer()
    {
        spriteScrew.sortingOrder = 202;
        spriteScrewPins.sortingOrder = 201;

    }
}
