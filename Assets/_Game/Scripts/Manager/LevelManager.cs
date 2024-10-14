using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public int indexLevel;
    public Level currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
        currentLevel.OnInit();
    }
}
