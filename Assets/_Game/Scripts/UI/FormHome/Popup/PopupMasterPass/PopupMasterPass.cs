using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupMasterPass : MonoBehaviour
{
    public RectTransform tf;

    public List<TaskMasterPassUI> taskMasterPassUIs;
    public List<MasterPassRewardDataUI> masterPassRewardDataUIs;

    public MasterPassRewardDataSO rewardDataSO;
    public MasterPassRewardDataSO rewardDataSO1;
    public TaskDataSO taskDataSO;
    public TaskMasterPassUI taskMasterPassUIPrefab;
    public MasterPassRewardDataUI rewardMasterPassUIPrefab;
    public Transform taskScroll;
    public Transform rewardScroll;

    public Transform buttonReward;
    public Transform buttonTask;
    public GameObject buttomUnlock;

    public GameObject taskContent;
    public GameObject rewardContent;

    public TMP_Text textLv;
    public TMP_Text textProgress;
    public TMP_Text textUnlock;
    public Image fillImage;
    public ParticleImage flyCoin;
    public ParticleImage flyGem;
    public ParticleImage xp_PI;
    public Transform flyAdd1;
    public Transform flyDelete;
    public Transform flyShuffle;
    public Transform flyUndo;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < taskMasterPassUIs.Count; i++)
        {
            Destroy(taskMasterPassUIs[i].gameObject);
        }
        taskMasterPassUIs.Clear();

        for (int i = 0; i < masterPassRewardDataUIs.Count; i++)
        {
            Destroy(masterPassRewardDataUIs[i].gameObject);
        }
        masterPassRewardDataUIs.Clear();

        for (int i = 0; i < taskDataSO.tasks.Count; i++)
        {
            TaskMasterPassUI task = Instantiate(taskMasterPassUIPrefab, taskScroll);
            taskMasterPassUIs.Add(task);
            task.OnInit(taskDataSO.tasks[i], xp_PI);
        }

        for (int i = 0; i < rewardDataSO.rewardDatas.Count; i++)
        {
            MasterPassRewardDataUI rewardDataUI = Instantiate(rewardMasterPassUIPrefab, rewardScroll);
            masterPassRewardDataUIs.Add(rewardDataUI);
            rewardDataUI.OnInit(rewardDataSO.rewardDatas[i], rewardDataSO1.rewardDatas[i], i);
        }
        textLv.text = "Lv." + (Mathf.Min(30, (DataManager.Ins.dataSaved.progress / 100) + 1));
        if (DataManager.Ins.dataSaved.progress / 100 < 29)
        {
            textProgress.text = (DataManager.Ins.dataSaved.progress % 100) + "/100";
            fillImage.fillAmount = (float)(DataManager.Ins.dataSaved.progress % 100) / 100;
        }
        else
        {
            textProgress.text = "max";
            fillImage.fillAmount = 1;
        }
        flyCoin.gameObject.SetActive(false);
        flyGem.gameObject.SetActive(false);

        ButtonReward();
    }

    public void UpdateProgress()
    {
        if (DataManager.Ins.dataSaved.progress % 100 == 0 && DataManager.Ins.dataSaved.progress < 2900)
        {
            DOVirtual.Float(((DataManager.Ins.dataSaved.progress % 100) + 80), 100f, 0.7f, (value) =>
            {
                textProgress.text = (int)value + "/100";
                fillImage.fillAmount = value / 100f;
            }).OnComplete(() =>
            {
                textProgress.text = 0 + "/100";
                textLv.text = "Lv." + ((DataManager.Ins.dataSaved.progress / 100) + 1);
                for (int i = 0; i < masterPassRewardDataUIs.Count; i++)
                {
                    masterPassRewardDataUIs[i].UpdateStatusUI();
                }
                DOVirtual.Float(1f, 0, 0.3f, (value) =>
                {
                    fillImage.fillAmount = value;
                });
            });
        }
        else if (DataManager.Ins.dataSaved.progress < 2900)
        {
            DOVirtual.Float(((DataManager.Ins.dataSaved.progress % 100) - 20), (DataManager.Ins.dataSaved.progress % 100), 0.7f, (value) =>
            {
                textProgress.text = (int)value + "/100";
                fillImage.fillAmount = value / 100f;
            });

        }
        else if (DataManager.Ins.dataSaved.progress == 2900)
        {
            DOVirtual.Float(((DataManager.Ins.dataSaved.progress % 100) + 80), 100f, 0.7f, (value) =>
            {
                textProgress.text = (int)value + "/100";
                fillImage.fillAmount = value / 100f;
            }).OnComplete(() =>
            {
                textProgress.text = "max";
                textLv.text = "Lv." + (Mathf.Min(30, (DataManager.Ins.dataSaved.progress / 100) + 1));
            });
        }
        else
        {
            textProgress.text = "max";
            textLv.text = "Lv." + (Mathf.Min(30, (DataManager.Ins.dataSaved.progress / 100) + 1));
        }
    }

    public void CollectCoinReward(Vector3 pos)
    {
        flyCoin.gameObject.SetActive(true);
        flyCoin.transform.position = pos;
        flyCoin.attractorTarget = UIManager.Ins.formHome.coinUI.IconTf;
        flyCoin.Play();
        UIManager.Ins.SetActiveBlock(true);
        UIManager.Ins.formHome.SetOverrideCoin(true);
        flyCoin.onLastParticleFinish.AddListener(() =>
        {
            UIManager.Ins.formHome.SetOverrideCoin(false);
            UIManager.Ins.SetActiveBlock(false);
            flyCoin.gameObject.SetActive(false);

        });
    }

    public void CollectGemReward(Vector3 pos)
    {
        flyGem.gameObject.SetActive(true);
        flyGem.transform.position = pos;
        flyGem.attractorTarget = UIManager.Ins.formHome.gemUI.IconTf;
        flyGem.Play();
        UIManager.Ins.SetActiveBlock(true);
        UIManager.Ins.formHome.SetOverrideGem(true);
        flyGem.onLastParticleFinish.AddListener(() =>
        {
            UIManager.Ins.formHome.SetOverrideGem(false);
            UIManager.Ins.SetActiveBlock(false);
            flyGem.gameObject.SetActive(false);
        });
    }

    public void CollectAdd1Reward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyAdd1.gameObject.SetActive(true);
        flyAdd1.transform.localScale = Vector3.one;
        flyAdd1.position = pos;
        flyAdd1.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1.1f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            flyAdd1.gameObject.SetActive(false);
        });
        DOVirtual.DelayedCall(0.8f, () =>
        {
            flyAdd1.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyAdd1.gameObject.SetActive(false);
            });
        });
    }

    public void CollectDeleteReward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyDelete.gameObject.SetActive(true);
        flyDelete.transform.localScale = Vector3.one;
        flyDelete.position = pos;
        flyDelete.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1.1f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            flyDelete.gameObject.SetActive(false);
        });
        DOVirtual.DelayedCall(0.8f, () =>
        {
            flyDelete.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyDelete.gameObject.SetActive(false);
            });
        });
    }

    public void CollectShuffleReward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyShuffle.gameObject.SetActive(true);
        flyShuffle.transform.localScale = Vector3.one;
        flyShuffle.position = pos;
        flyShuffle.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1.1f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            flyShuffle.gameObject.SetActive(false);
        });
        DOVirtual.DelayedCall(0.8f, () =>
        {
            flyShuffle.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyShuffle.gameObject.SetActive(false);
            });
        });
    }
    public void CollectUndoReward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyUndo.gameObject.SetActive(true);
        flyUndo.transform.localScale = Vector3.one;
        flyUndo.position = pos;
        flyUndo.DOMove(UIManager.Ins.formHome.buttonPlay.position, 1.1f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            flyUndo.gameObject.SetActive(false);
        });
        DOVirtual.DelayedCall(0.8f, () =>
        {
            flyUndo.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyUndo.gameObject.SetActive(false);
            });
        });
    }

    public void ButtonReward()
    {
        buttonReward.DOScale(Vector3.one * 1.1f, 0.3f);
        buttonTask.DOScale(Vector3.one, 0.3f);

        for (int i = 0; i < masterPassRewardDataUIs.Count; i++)
        {
            masterPassRewardDataUIs[i].UpdateStatusUI();
        }
        taskContent.SetActive(false);
        rewardContent.SetActive(true);
        buttomUnlock.SetActive(!DataManager.Ins.dataSaved.unlockedMasterPass);
        textUnlock.text = DataManager.Ins.dataSaved.ticketUnlockMasterPass + "/1";
    }

    public void ButtonTask()
    {
        buttonReward.DOScale(Vector3.one, 0.3f);
        buttonTask.DOScale(Vector3.one * 1.1f, 0.3f);

        taskContent.SetActive(true);
        rewardContent.SetActive(false);
        buttomUnlock.SetActive(false);
    }

    public void ButtonUnlock()
    {
        if (DataManager.Ins.dataSaved.ticketUnlockMasterPass > 0)
        {
            DataManager.Ins.dataSaved.unlockedMasterPass = true;
            DataManager.Ins.dataSaved.ticketUnlockMasterPass--;
            for (int i = 0; i < masterPassRewardDataUIs.Count; i++)
            {
                masterPassRewardDataUIs[i].UpdateStatusUI();
            }
            buttomUnlock.SetActive(!DataManager.Ins.dataSaved.unlockedMasterPass);
        }
    }
    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
