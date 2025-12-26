using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskMasterPassUI : MonoBehaviour
{
    public Image fill;
    public TMP_Text task;
    public TMP_Text textProgress;
    public GameObject buttonGO;
    public GameObject buttonClaim;
    public GameObject complete;
    public Transform xpTF;

    public ParticleImage xp_PI;
    public TaskData taskData;
    public void OnInit(TaskData taskData, ParticleImage xp_PI)
    {
        this.taskData = taskData;
        this.xp_PI = xp_PI;
        switch (taskData.taskType)
        {
            case TaskType.PlayLevel:
                textProgress.text = Mathf.Min(DataManager.Ins.dataSaved.nPlayGame, taskData.target) + "/" + taskData.target;
                fill.transform.localScale = new Vector3((float)Mathf.Min(DataManager.Ins.dataSaved.nPlayGame, taskData.target) / taskData.target, 1f, 1f);
                fill.pixelsPerUnitMultiplier = (float)Mathf.Min(DataManager.Ins.dataSaved.nPlayGame, taskData.target) / taskData.target;
                if (Mathf.Min(DataManager.Ins.dataSaved.nPlayGame, taskData.target) < taskData.target)
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(true);
                    complete.SetActive(false);
                }
                else if (DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index])
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(false);
                    complete.SetActive(true);
                }else
                {
                    buttonClaim.SetActive(true);
                    buttonGO.SetActive(false);
                    complete.SetActive(false);
                }
                    break;
            case TaskType.CompleteLevel:
                textProgress.text = Mathf.Min(DataManager.Ins.dataSaved.nWinGame, taskData.target) + "/" + taskData.target;
                fill.transform.localScale = new Vector3((float)Mathf.Min(DataManager.Ins.dataSaved.nWinGame, taskData.target) / taskData.target, 1f, 1f);
                fill.pixelsPerUnitMultiplier = (float)Mathf.Min(DataManager.Ins.dataSaved.nWinGame, taskData.target) / taskData.target;
                if (Mathf.Min(DataManager.Ins.dataSaved.nWinGame, taskData.target) < taskData.target)
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(true);
                    complete.SetActive(false);
                }
                else if (DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index])
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(false);
                    complete.SetActive(true);
                }
                else
                {
                    buttonClaim.SetActive(true);
                    buttonGO.SetActive(false);
                    complete.SetActive(false);
                }
                break;
            case TaskType.UseCoin:
                textProgress.text = Mathf.Min(DataManager.Ins.dataSaved.nUseCoin, taskData.target) + "/" + taskData.target;
                fill.transform.localScale = new Vector3((float)Mathf.Min(DataManager.Ins.dataSaved.nUseCoin, taskData.target) / taskData.target, 1f, 1f);
                fill.pixelsPerUnitMultiplier = (float)Mathf.Min(DataManager.Ins.dataSaved.nUseCoin, taskData.target) / taskData.target;
                if (Mathf.Min(DataManager.Ins.dataSaved.nUseCoin, taskData.target) < taskData.target)
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(true);
                    complete.SetActive(false);
                }
                else if (DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index])
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(false);
                    complete.SetActive(true);
                }
                else
                {
                    buttonClaim.SetActive(true);
                    buttonGO.SetActive(false);
                    complete.SetActive(false);
                }
                break;
            case TaskType.UseGems:
                textProgress.text = Mathf.Min(DataManager.Ins.dataSaved.nUseGem, taskData.target) + "/" + taskData.target;
                fill.transform.localScale = new Vector3((float)Mathf.Min(DataManager.Ins.dataSaved.nUseGem, taskData.target) / taskData.target, 1f, 1f);
                fill.pixelsPerUnitMultiplier = (float)Mathf.Min(DataManager.Ins.dataSaved.nUseGem, taskData.target) / taskData.target;
                if (Mathf.Min(DataManager.Ins.dataSaved.nUseGem, taskData.target) < taskData.target)
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(true);
                    complete.SetActive(false);
                }
                else if (DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index])
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(false);
                    complete.SetActive(true);
                }
                else
                {
                    buttonClaim.SetActive(true);
                    buttonGO.SetActive(false);
                    complete.SetActive(false);
                }
                break;
            case TaskType.UseBooster:
                textProgress.text = Mathf.Min(DataManager.Ins.dataSaved.nUseBooster, taskData.target) + "/" + taskData.target;
                fill.transform.localScale = new Vector3((float)Mathf.Min(DataManager.Ins.dataSaved.nUseBooster, taskData.target) / taskData.target, 1f, 1f);
                fill.pixelsPerUnitMultiplier = (float)Mathf.Min(DataManager.Ins.dataSaved.nUseBooster, taskData.target) / taskData.target;
                if (Mathf.Min(DataManager.Ins.dataSaved.nUseBooster, taskData.target) < taskData.target)
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(true);
                    complete.SetActive(false);
                }
                else if (DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index])
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(false);
                    complete.SetActive(true);
                }
                else
                {
                    buttonClaim.SetActive(true);
                    buttonGO.SetActive(false);
                    complete.SetActive(false);
                }
                break;
            case TaskType.CompleteMinigame:
                textProgress.text = Mathf.Min(DataManager.Ins.dataSaved.nWinChallenge, taskData.target) + "/" + taskData.target;
                fill.transform.localScale = new Vector3((float)Mathf.Min(DataManager.Ins.dataSaved.nWinChallenge, taskData.target) / taskData.target, 1f, 1f);
                fill.pixelsPerUnitMultiplier = (float)Mathf.Min(DataManager.Ins.dataSaved.nWinChallenge, taskData.target) / taskData.target;
                if (Mathf.Min(DataManager.Ins.dataSaved.nWinChallenge, taskData.target) < taskData.target)
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(true);
                    complete.SetActive(false);
                }
                else if (DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index])
                {
                    buttonClaim.SetActive(false);
                    buttonGO.SetActive(false);
                    complete.SetActive(true);
                }
                else
                {
                    buttonClaim.SetActive(true);
                    buttonGO.SetActive(false);
                    complete.SetActive(false);
                }
                break;
        }

        task.text = taskData.task;
    }

    public void Claim()
    {
        DataManager.Ins.dataSaved.taskMasterPassStatus[taskData.index] = true;
        DataManager.Ins.dataSaved.progress += 20;
        PlayXpPI();
        UIManager.Ins.formHome.popupMasterPass.UpdateProgress();
        UpdateStateClaimed();
    }

    public void Go()
    {
        switch (taskData.taskType)
        {
            case TaskType.PlayLevel:
                UIManager.Ins.formHome.popupMasterPass.Close();
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    UIManager.Ins.formHome.LoadGame();
                });
                break;
            case TaskType.CompleteLevel:
                UIManager.Ins.formHome.popupMasterPass.Close();
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    UIManager.Ins.formHome.LoadGame();
                });
                break;
            case TaskType.UseCoin:
                UIManager.Ins.formHome.popupMasterPass.Close();
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    UIManager.Ins.formHome.OpenPopupShop();
                });
                break;
            case TaskType.UseGems:
                UIManager.Ins.formHome.popupMasterPass.Close();
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    UIManager.Ins.formHome.OpenPopupShop();
                });
                break;
            case TaskType.UseBooster:
                UIManager.Ins.formHome.popupMasterPass.Close();
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    UIManager.Ins.formHome.LoadGame();
                });
                break;
            case TaskType.CompleteMinigame:
                UIManager.Ins.formHome.popupMasterPass.Close();
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    UIManager.Ins.formHome.OpenPopupDailyChallenge();
                });
                break;
        }
    }

    public void PlayXpPI()
    {
        xp_PI.transform.position = xpTF.position;
        xp_PI.Play();

    }

    public void UpdateStateClaimed()
    {
        buttonClaim.SetActive(false);
        buttonGO.SetActive(false);
        complete.SetActive(true);
    }

}
