using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskDataScriptableObject", menuName = "ScriptableObjects/TaskDataScriptableObject", order = 1)]
public class TaskDataSO : ScriptableObject
{
    public List<TaskData> tasks;
}

[System.Serializable]
public class TaskData
{
    public int index;
    public int target;
    public string task;
    public TaskType taskType;
}

public enum TaskType
{
    PlayLevel,
    CompleteLevel,
    UseBooster,
    UseCoin,
    UseGems,
    CompleteMinigame
}
