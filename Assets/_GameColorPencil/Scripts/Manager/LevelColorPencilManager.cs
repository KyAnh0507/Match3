using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class LevelManagerColorPencil : Singleton<LevelManagerColorPencil>
{
    public List<LevelColorPencil> levels;
    public ColorDatas colorDatas;

    internal LevelColorPencil currentLevel;
    private int indexLevel;

    public bool isNextLevel;
    private void Start()
    {
        LoadLevel();
        UIManagerColorPencil.Ins.OpenUI<UIGamePlay>();
    }

    // Tải level
    public void LoadLevel()
    {
        if (currentLevel != null)
        {
            currentLevel.Despawn();
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[DataManager.Ins.dataSaved.indexLevelColorPencil % levels.Count]);
        currentLevel.OnInit();

        GamePlayColorPencil.Ins.canPlay = true;
    }

    public void Victory()
    {
        GamePlayColorPencil.Ins.canPlay = false;
        DOVirtual.DelayedCall(1f, () =>
        {
            UIManagerColorPencil.Ins.OpenUI<UIWinColorPencil>();
        });
    }

    public void Defeat()
    {
        GamePlayColorPencil.Ins.canPlay = false;

        DataManager.Ins.dataSaved.completeChallenge = true;

        DOVirtual.DelayedCall(1.5f, () =>
        {
            UIManagerColorPencil.Ins.ChangeScene(Scene.Home);
        });
    }

    public int MaxNPencil()
    {
        if (currentLevel != null)
        {
            int m = currentLevel.rowPencils.Max(a => a.numberBox);
            return m;
        }
        return 1;
    }
}
