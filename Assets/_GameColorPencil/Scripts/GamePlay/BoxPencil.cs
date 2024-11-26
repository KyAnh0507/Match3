using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPencil : GameUnit
{
    public SpriteRenderer sprite;

    public GameObject tick;
    public Blank blank;

    public bool hasPencil;
    public bool correctColor;
    public Pencil pencil;

    public int nBoxPencil;
    public int layer;

    public int color;

    public void OnInit(int n, ColorType colorType, bool hasPencil = true)
    {
        layer = n * 100 + nBoxPencil * 8 + 6;
        sprite.sortingOrder = layer;
        blank.OnInit(layer - 4);

        this.hasPencil = hasPencil;
        this.correctColor = false;

        SetColor(colorType);
        color = (int)colorType;

        if (hasPencil)
        {
            pencil = SimplePool.Spawn<Pencil>(PoolType.Pencil, TF.transform.position, Quaternion.identity);
            LevelManagerColorPencil.Ins.currentLevel.pencils.Add(pencil);
            pencil.OnInit(layer - 2);
            pencil.SetColor((ColorType)RandomColor());
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPencil(Pencil pencil)
    {
        this.pencil = pencil;

        if (pencil == null)
        {
            hasPencil = false;
            correctColor = true;
        }
        else if (this.color != this.pencil.color)
        {
            hasPencil = true;
            correctColor = false;
        }
        else
        {
            hasPencil = true;
            correctColor = true;
        }
    }

    public void SelectPencil()
    {
        if (pencil != null)
        {
            pencil.SelectPencil(TF.position + GamePlayColorPencil.Ins.selectPosition, 0.3f);
        }
    }

    public void UnSelectPencil()
    {
        if (pencil != null)
        {
            pencil.SelectPencil(TF.position, 0.3f);
        }
    }

    public void SetLayerPencil()
    {
        pencil.SetLayer(layer - 2);
    }

    public void SetColor(ColorType type)
    {
        sprite.color = LevelManagerColorPencil.Ins.colorDatas.GetColor(type);
        blank.sprite.color = LevelManagerColorPencil.Ins.colorDatas.GetColor(type);
    }

    public void SetActiveTick(bool b)
    {
        tick.SetActive(b);
    }

    public int RandomColor()
    {
        int random = 0;

        for (int i = 0; i < 20; i++)
        {
            random = (int)Random.Range(0, LevelManagerColorPencil.Ins.currentLevel.randomColor.Count);
            if (LevelManagerColorPencil.Ins.currentLevel.randomColor[random] != color)
            {
                break;
            }
        }

        int type = LevelManagerColorPencil.Ins.currentLevel.randomColor[random];
        LevelManagerColorPencil.Ins.currentLevel.randomColor.RemoveAt(random);
        return type;
    }
}
