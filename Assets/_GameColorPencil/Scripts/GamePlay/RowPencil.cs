using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RowPencil : MonoBehaviour
{
    public List<BoxPencil> boxPencil;

    public Transform tf;
    public float posY;
    public int nRow;
    public int numberBox;
    public ColorType colorType;
    public bool completeRow;

    public void OnInit()
    {
        posY = tf.position.y;
        if (numberBox % 2 == 0)
        {
            for (int i = 0; i < numberBox; i++)
            {
                BoxPencil box = SimplePool.Spawn<BoxPencil>(PoolType.BoxPencil, 
                                            tf.position + Vector3.right * ((numberBox / 2 - i - 0.5f) * 1.038f),
                                            quaternion.identity);
                boxPencil.Add(box);
                box.nBoxPencil = i;
                if (nRow == 0 && i == 0)
                {
                    box.OnInit(nRow, colorType, false);
                }
                else
                {
                    box.OnInit(nRow, colorType);
                }
            }
        }
        else
        {
            for (int i = 0; i < numberBox; i++)
            {
                BoxPencil box = SimplePool.Spawn<BoxPencil>(PoolType.BoxPencil,
                                            tf.position + Vector3.right * ((numberBox / 2 - i) * 1.038f),
                                            quaternion.identity);
                boxPencil.Add(box);
                box.nBoxPencil = i;
                if (nRow == 0 && i == 0)
                {
                    box.OnInit(nRow, colorType, false);
                }
                else
                {
                    box.OnInit(nRow, colorType);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCorrectColor() && !completeRow)
        {
            completeRow = true;
            DOVirtual.DelayedCall(1f, () =>
            {
                ShowTick();
            });
        }
        else if (!CheckCorrectColor())
        {
            completeRow = false;
        }
    }

    public bool CheckCorrectColor()
    {
        int n = boxPencil.Count;
        if (nRow != 0)
        {
            for (int i = 0; i < n; i++)
            {
                if (!boxPencil[i].correctColor || !boxPencil[i].hasPencil)
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                if (!boxPencil[i].correctColor)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    public void ShowTick()
    {
        int n = boxPencil.Count;
        for (int i = 0; i < n; i++)
        {
            if (boxPencil[i].hasPencil)
            {
                boxPencil[i].SetActiveTick(true);
            }
        }

        DOVirtual.DelayedCall(1f, () =>
        {
            HideTick();
        });
    }

    public void HideTick()
    {
        int n = boxPencil.Count;
        for (int i = 0; i < n; i++)
        {
            boxPencil[i].SetActiveTick(false);
        }
    }

    public void DespawnAllBoxPencil()
    {
        for (int i = 0; i < boxPencil.Count; i++)
        {
            SimplePool.Despawn(boxPencil[i]);
        }
    }
}
