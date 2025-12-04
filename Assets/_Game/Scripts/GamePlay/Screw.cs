using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Screw : GameUnit
{
    public static Vector2 Size = new Vector2(0.765f, 0.5f);


    public int layer;
    public int screwType;
    public Transform imageScrewPins;
    public Transform imageScrew;
    public Vector3 positionButton;
    public bool canPlay = true;
    public Collider2D collider2D;
    public Rigidbody2D rb;
    public SpriteRenderer spriteScrew;
    public SpriteRenderer spriteScrewPins;
    public Vector3 posStart;
    public bool isMatching = false;
    public float speed = 5f;

    public bool IsMoving => moveTweenX.IsActive() || moveTweenY.IsActive();
    private Tween moveTweenX;
    private Tween moveTweenY;
    public void OnInit(int layer)
    {
        positionButton = transform.localPosition;
        this.layer = layer;
        ChangeLayer(layer);
        posStart = transform.position;
        EnableCollider();
        canPlay = true;
    }
    
    public void Move(Vector3 pos)
    {
        canPlay = false;

        PullUp(pos);
    }

    public void Move1(Vector3 pos)
    {
        if(!isMatching)
        {
            TF.DOKill();
        }
        TF.DOMove(pos, 0.2f).OnComplete(() =>
        {
            
        });
    }

    public void MoveX(Vector3 pos)
    {
        if (!isMatching)
        {
            moveTweenX.Kill();
        }
        moveTweenX = imageScrew.DOLocalMoveX(pos.x, 0.2f).OnComplete(() =>
        {

        });
    }

    public void MoveX1(Vector3 pos, float duration)
    {
        moveTweenX.Kill();
        moveTweenX = imageScrew.DOLocalMoveX(pos.x, duration).OnComplete(() =>
        {

        });
    }
    public void PullUp(Vector3 pos)
    {
        ChangeSortingLayer();
        moveTweenY = imageScrew.DOLocalMoveY(imageScrew.transform.localPosition.y + 0.5f, 0.3f);
        imageScrewPins.DOLocalMoveY(-0.25f, 0.3f).OnComplete(() =>
        {
            float distance = Vector3.Distance(transform.localPosition, pos);
            if (Mathf.Approximately(distance, 0f))
            {
                return;
            }
            float duration = distance / speed;
            if (duration > 10) duration = 2f;
            if (duration < 0.13f) duration *= 1.5f;

            moveTweenX = imageScrew.DOLocalMoveX(pos.x, duration).SetEase(Ease.Linear);
            moveTweenY = imageScrew.DOLocalMoveY(pos.y + 0.5f, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                PullDown(pos);
            });
        });
    }

    public void PullDown(Vector3 pos)
    {
        DOVirtual.DelayedCall(0.08f, () =>
        {
            moveTweenY = imageScrew.DOLocalMoveY(pos.y + 0.1f, 0.3f);
            imageScrewPins.DOLocalMove(new Vector3(0, 0, 0), 0.3f);
        });
    }

    public void Match3()
    {
        isMatching = true;
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

    public void ChangeScrewType1(int screwType)
    {
        spriteScrew.color = CreateLeveManager.ins.colors[screwType];
        spriteScrewPins.color = CreateLeveManager.ins.colors[screwType];
        this.screwType = screwType;
    }
    public void ChangeLayer(int layer)
    {
        gameObject.layer = layer + 6;
        spriteScrew.sortingOrder = layer * 10 + 2;
        spriteScrewPins.sortingOrder = layer * 10 + 1;
    }

    public void ChangeSortingLayer()
    {
        spriteScrew.sortingOrder = 202;
        spriteScrewPins.sortingOrder = 201;

    }

    public void Undo()
    {
        TF.DOKill();
        TF.position = posStart;
        TF.rotation = Quaternion.identity;
        canPlay = true;
        EnableCollider();
    }

    public void Disappear()
    {
        transform.DOScale(0f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SimplePool.Despawn(this);
        });
        transform.DORotate(new Vector3(0, 0, 360f), 0.2f, RotateMode.FastBeyond360);
    }

    public void EnableCollider()
    {
        collider2D.enabled = true;
    }

    public void DisableCollider()
    {
        collider2D.enabled = false;
    }

    public void SetParent(Transform parent)
    {
        if (transform.parent == parent)
        {
            return;
        }
        transform.SetParent(parent, true);
    }
}
