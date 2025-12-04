using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelColorPencil : MonoBehaviour
{
    public List<RowPencil> rowPencils;
    public List<Pencil> pencils;

    public List<int> randomColor;

    public bool completeLevel;
    public void OnInit()
    {
        for (int i = 0; i < rowPencils.Count; i++)
        {
            for (int j = 0; j < rowPencils[i].numberBox; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                randomColor.Add((int)rowPencils[i].colorType);
            }
        }

        completeLevel = false;
        for (int i = 0; i < rowPencils.Count; i++)
        {
            rowPencils[i].OnInit();
        }

        CamCPControl.Ins.SetCameraSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckWin() && !completeLevel)
        {
            completeLevel = true;
            GamePlayColorPencil.Ins.canPlay = false;
            DOVirtual.DelayedCall(1f, () =>
            {
                LevelManagerColorPencil.Ins.Victory();
            });
        }else if (!CheckWin())
        {
            completeLevel = false;
        }
    }

    public bool CheckWin()
    {
        int n = rowPencils.Count;
        for (int i = 0; i < n; i++)
        {
            if (!rowPencils[i].completeRow)
            {
                return false;
            }
        }
        return true;
    }

    public void Despawn()
    {
        for (int i = 0; i < pencils.Count; i++)
        {
            SimplePool.Despawn(pencils[i]);
        }
        
        for (int i = 0; i < rowPencils.Count; i++)
        {
            rowPencils[i].DespawnAllBoxPencil();
        }
    }
}
