using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Level : MonoBehaviour
{
    public QueueTile queueTile;

    public List<Screw> screws;
    public List<Iron> irons;
    public List<Hole1Iron> hole1Irons;
    public List<Hole2Iron> hole2Irons;

    public Hole1Iron hole1Iron;

    public bool isDefeatChecked;
    public int numberColor;
    [Header("Super Hard")]
    public int superHardTime;

    public void OnInit()
    {
        for (int i = 0; i < hole2Irons.Count; i++)
        {
            hole2Irons[i].OnInit(this);

            Collider2D[] icols = Physics2D.OverlapPointAll(hole2Irons[i].transform.position, GamePlay.Ins.ironLayerMask);
            if (icols.Length > 0)
            {
                int n = icols.Length;
                for (int j = 0; j < n; j++)
                {
                    Iron iron = Cache.GetIron(icols[j]);
                    if (iron != null)
                    {
                        Hole1Iron h = Instantiate(hole1Iron, hole2Irons[i].transform.position, Quaternion.identity);
                        h.SetParent(iron.transform);
                        h.SetOrderInLayer(4 + iron.layer * 2);
                        h.SetRotation();
                        h.SetScale(1 / iron.transform.localScale.x, 1 / iron.transform.localScale.y);
                        iron.hole1Irons.Add(h);
                    }
                }
            }
        }

        for (int j = 0; j < irons.Count; j++)
        {
            for (int i = 0; i < irons[j].hole1Irons.Count; i++)
            {
                irons[j].hole1Irons[i].OnInit(this);

                Screw_Hole screw_Hole = new Screw_Hole(irons[j].hole1Irons[i].screw, irons[j].hole1Irons[i]);
                irons[j].screws_holes.Add(screw_Hole);
            }
        }
        
        //queueTile
        isDefeatChecked = false;

        GameManager.Ins.ChangeState(GameState.GAMEPLAY);
    }

    private void Update()
    {
        if (GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            /*if (CheckDefeatCondition() && !isDefeatChecked)
            {
                StartCoroutine(CheckDefeatContinuously());
            }*/
        }
    }

    /*private IEnumerator CheckDefeatContinuously()
    {
        isDefeatChecked = true;
        float endTime = Time.time + 3f;

        while (Time.time < endTime)
        {
            if ((!CheckDefeatCondition() && UIManager.Ins.formGame.canLose) || !isDefeatChecked)
            {
                Debug.Log("hit");
                isDefeatChecked = false;
                yield break; // Ngừng coroutine nếu điều kiện thỏa mãn
            }
            yield return null; // Chờ cho đến khung hình tiếp theo
        }
        Debug.Log("hit");

        LevelManager.instance.StartAdsEffect();
        LevelManager.instance.countText.gameObject.SetActive(true);
        LevelManager.instance.countTween = DOVirtual.Int(5, 0, 5f, value =>
        {
            LevelManager.instance.countText.text = value.ToString();
        }).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (LevelManager.instance.currentLevel.isDefeatChecked)
            {
                LevelManager.instance.Defeat();
            }
        });

        endTime = Time.time + 5f;
        while (Time.time < endTime)
        {
            if ((!CheckDefeatCondition() && UIManager.Ins.formGame.canLose) || !isDefeatChecked)
            {
                LevelManager.Ins.ResetCountTween();
                isDefeatChecked = false;
                yield break; // Ngừng coroutine nếu điều kiện thỏa mãn
            }
            yield return null; // Chờ cho đến khung hình tiếp theo
        }
    }*/

    // 

    public bool CheckDefeatCondition()
    {
        if (DataManager.Ins.dataSaved.isSuperHard) return false;

        // Kiểm tra điều kiện thua và trả về true nếu thỏa mãn
        for (int i = 0; i < hole1Irons.Count; i++)
        {
            /*if (!holes[i].hasScrew && !holes[i].hasLock && !holes[i].hasAdsHole)
            {
                Collider2D[] icol = Physics2D.OverlapCircleAll(holes[i].transform.position, 0.05f, GamePlay.Ins.ironLayerMask);
                Collider2D[] ihcol = Physics2D.OverlapPointAll(holes[i].transform.position, GamePlay.Ins.ironHoleLayerMask);
                if (icol.Length == 0 || icol.Length == ihcol.Length)
                {
                    return false;
                }
            }*/
        }
        return true;
    }
}
