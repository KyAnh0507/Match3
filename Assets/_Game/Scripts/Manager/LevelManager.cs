using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public int indexLevel;
    public Level currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
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

        currentLevel = Instantiate(levels[level]);
        currentLevel.OnInit();
        UIManager.Ins.formGame.isPauseGame = false;
        GameManager.Ins.ChangeState(GameState.GAMEPLAY);

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
    }

    public void Victory()
    {
        GameManager.Ins.ChangeState(GameState.FINISH);
        DataManager.Ins.dataSaved.timeRetry = 0;
        DataManager.Ins.dataSaved.indexLevel++;
        UIManager.Ins.formGame.OpenPopupWin();
    }



    // Thua game
    public void Defeat()
    {
        GameManager.Ins.ChangeState(GameState.FINISH);
        DataManager.Ins.dataSaved.timeRetry++;
        UIManager.Ins.formGame.OpenPopupLose();
    }
}
