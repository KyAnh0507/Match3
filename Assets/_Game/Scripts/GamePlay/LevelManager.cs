using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public List<LevelGameModel> levelGameModels = new List<LevelGameModel>();
    public int indexLevel;
    public Level currentLevel;

    public Level levelPrefab;
    public List<Iron> ironPrefabs;
    public Hole1Iron hole1ironPrefab;
    public Transform ironParent;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(DataManager.Ins.dataSaved.indexLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int level)
    {
        indexLevel = level;
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        /*currentLevel = Instantiate(levels[level]);
        currentLevel.OnInit();*/

        currentLevel = Instantiate(levelPrefab);
        ironParent = currentLevel.ironParent;
        int d = 0;
        for (int i = 0; i < levelGameModels[level].levelModel.ironModes.Count; i++)
        {
            Iron iron = Instantiate(ironPrefabs[levelGameModels[level].levelModel.ironModes[i].id], ironParent);
            iron.transform.position = levelGameModels[level].levelModel.ironModes[i].transModel.position * 0.3f + (float3)Vector3.up * 1f;
            iron.transform.rotation = Quaternion.Euler(levelGameModels[level].levelModel.ironModes[i].transModel.rotation);
            iron.transform.localScale = levelGameModels[level].levelModel.ironModes[i].transModel.localScale;

            iron.layer = levelGameModels[level].levelModel.ironModes[i].layer;
            iron.transform.gameObject.layer = 12 + iron.layer;
            iron.polygonCollider = iron.transform.AddComponent<PolygonCollider2D>();
            iron.polygonCollider.pathCount = 1;
            iron.polygonCollider.SetPath(0, levelGameModels[level].levelModel.ironModes[i].polygonColliderPoints);

            for (int j = 0; j < levelGameModels[level].levelModel.ironModes[i].holeModels.Count; j++)
            {
                Hole1Iron hole1Iron = Instantiate(hole1ironPrefab, iron.transform);
                hole1Iron.transform.localPosition = levelGameModels[level].levelModel.ironModes[i].holeModels[j].transModel.position;
                hole1Iron.transform.localRotation = Quaternion.Euler(levelGameModels[level].levelModel.ironModes[i].holeModels[j].transModel.rotation);
                hole1Iron.transform.localScale = levelGameModels[level].levelModel.ironModes[i].holeModels[j].transModel.localScale;

                hole1Iron.screwType = levelGameModels[level].levelModel.ironModes[i].holeModels[j].screwType;
                hole1Iron.hasScrew = levelGameModels[level].levelModel.ironModes[i].holeModels[j].hasScrew;
                hole1Iron.layer = iron.layer;
                iron.hole1Irons.Add(hole1Iron);
                d++;
            }

            currentLevel.irons.Add(iron);
        }

        for (int i = 0; i < currentLevel.irons.Count; i++)
        {
            currentLevel.irons[i].ChangeLayer();
            currentLevel.irons[i].loaded = true;
        }
        RangeCheckIron.Instance.level = currentLevel;
        
        currentLevel.OnInit();
        currentLevel.targetMatch = d / 3;
        UIManager.Ins.formGame.isPauseGame = false;
        UIManager.Ins.formGame.ResumeGame();
        GameManager.Ins.ChangeState(GameState.GAMEPLAY);
        UIManager.Ins.formGame.textLevel.text = Constant.LEVEL + " " + (DataManager.Ins.dataSaved.indexLevel + 1).ToString();
        DataManager.Ins.dataSaved.attenpt++;
        DataManager.Ins.dataSaved.remainboosteradd1 = 0;
        DataManager.Ins.dataSaved.remainboostershuffle = 0;
        DataManager.Ins.dataSaved.remainboosterdelete = 0;
        DataManager.Ins.dataSaved.remainboosterundo = 0;
        DataManager.Ins.dataSaved.nPlayGame++;
        FirebaseManager.Ins.level_start((indexLevel + 1) + "", DataManager.Ins.dataSaved.attenpt);
    }
    public void LoadLevel()
    {
        indexLevel = DataManager.Ins.dataSaved.indexLevel;
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[indexLevel]);
        currentLevel.OnInit();
        UIManager.Ins.formGame.isPauseGame = false;
        UIManager.Ins.formGame.ResumeGame();
        GameManager.Ins.ChangeState(GameState.GAMEPLAY);

        UIManager.Ins.formGame.textLevel.text = Constant.LEVEL + " " + (DataManager.Ins.dataSaved.indexLevel + 1).ToString();
    }

    public void Victory()
    {
        GameManager.Ins.ChangeState(GameState.FINISH);
        DataManager.Ins.dataSaved.timeRetry = 0;
        DataManager.Ins.dataSaved.indexLevel++;
        DataManager.Ins.dataSaved.level++;
        DataManager.Ins.dataSaved.attenpt = 0;
        DataManager.Ins.dataSaved.nWinGame++;
        FirebaseManager.Ins.level_end((indexLevel + 1) + "", "win", DataManager.Ins.dataSaved.remainboosteradd1, DataManager.Ins.dataSaved.remainboostershuffle, DataManager.Ins.dataSaved.remainboosterdelete,
                                       DataManager.Ins.dataSaved.remainboosterundo, DataManager.Ins.dataSaved.currentWinstreak);
        UIManager.Ins.formGame.OpenPopupWin();
    }



    // Thua game
    public void Defeat()
    {
        GameManager.Ins.ChangeState(GameState.FINISH);
        DataManager.Ins.dataSaved.timeRetry++;
        FirebaseManager.Ins.level_end((indexLevel + 1) + "", "lose", DataManager.Ins.dataSaved.remainboosteradd1, DataManager.Ins.dataSaved.remainboostershuffle, DataManager.Ins.dataSaved.remainboosterdelete,
                                       DataManager.Ins.dataSaved.remainboosterundo, DataManager.Ins.dataSaved.currentWinstreak);
        UIManager.Ins.formGame.OpenPopupLose();
    }
}
