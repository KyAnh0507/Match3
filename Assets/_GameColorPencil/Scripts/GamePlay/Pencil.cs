using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : GameUnit
{
    public SpriteRenderer sprite;

    public int color;
    public void OnInit(int n)
    {
        sprite.sortingOrder = n;
    }

    public void Move(Vector3 pos1, Vector3 pos2, int layer1, int layer2)
    {
        TF.DOKill();
        GamePlayColorPencil.Ins.canPlay = false;
        TF.DOMove(pos1 + GamePlayColorPencil.Ins.movePosition, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetLayer(layer1);
            TF.DOMove(pos2 + GamePlayColorPencil.Ins.movePosition, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                SetLayer(layer2);
                TF.DOMove(pos2, 0.2f).OnComplete(() =>
                {
                    GamePlayColorPencil.Ins.canPlay = true;
                });
            });
        });
    }


    public void SelectPencil(Vector3 pos, float t)
    {
        GamePlayColorPencil.Ins.canPlay = false;
        TF.DOMove(pos, t).OnComplete(() =>
        {
            GamePlayColorPencil.Ins.canPlay = true;
        });
    }

    public void SetLayer(int n)
    {
        sprite.sortingOrder = n;
    }

    public void SetColor(ColorType colorType)
    {
        sprite.color = LevelManagerColorPencil.Ins.colorDatas.GetColor(colorType);
        color = (int)colorType;
    }
}
