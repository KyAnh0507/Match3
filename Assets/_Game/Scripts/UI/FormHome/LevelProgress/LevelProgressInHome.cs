using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelProgressInHome : MonoBehaviour
{
    public List<UILevelProgressInHome> uiLevelProgressInHomes;
    public RectTransform content;
    public Transform parent;
    public GameObject prefabUIProgress;
    public float lerpSpeed = 5f;
    public Vector2 targetPosition;
    public bool isDrag = false;
    private void OnEnable()
    {
        int indexLevel = DataManager.Ins.dataSaved.indexLevel + 1;
        for (int i = 0; i < 11 + Mathf.Min(10, indexLevel - 1); i++)
        {
            GameObject ui = Instantiate(prefabUIProgress, parent);
            UILevelProgressInHome uiProgress = ui.GetComponent<UILevelProgressInHome>();
            uiLevelProgressInHomes.Add(uiProgress);
            if (indexLevel < 11)
            {
                if (i < indexLevel - 1)
                {
                    uiProgress.levelFinished.SetActive(true);
                    uiProgress.currentLevel.SetActive(false);
                    uiProgress.levelUnFinish.SetActive(false);
                    uiProgress.fill.SetActive(true);
                }else if (i == indexLevel - 1)
                {
                    uiProgress.levelFinished.SetActive(false);
                    uiProgress.currentLevel.SetActive(true);
                    uiProgress.levelUnFinish.SetActive(false);
                    //targetPosition = content.anchoredPosition;
                }
                else
                {
                    uiProgress.levelFinished.SetActive(false);
                    uiProgress.currentLevel.SetActive(false);
                    uiProgress.levelUnFinish.SetActive(true);
                }
                uiProgress.indexLevel.text = (i+1).ToString();
            }
            else
            {
                if (i < 10)
                {
                    uiProgress.levelFinished.SetActive(true);
                    uiProgress.currentLevel.SetActive(false);
                    uiProgress.levelUnFinish.SetActive(false);
                    uiProgress.fill.SetActive(true);
                }
                else if (i == 10)
                {
                    uiProgress.levelFinished.SetActive(false);
                    uiProgress.currentLevel.SetActive(true);
                    uiProgress.levelUnFinish.SetActive(false);
                    //targetPosition = content.anchoredPosition;
                }
                else
                {
                    uiProgress.levelFinished.SetActive(false);
                    uiProgress.currentLevel.SetActive(false);
                    uiProgress.levelUnFinish.SetActive(true);
                }
                uiProgress.indexLevel.text = (indexLevel - 10 + i).ToString();
            }
        }
        targetPosition = new Vector2(0, 2147.052f);
    }

    private void Update()
    {
        if (!isDrag)
        {
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, lerpSpeed * Time.deltaTime);
        }
    }

    public void Drag()
    {
        isDrag = true;
    }

    public void Drop()
    {
        isDrag = false;
    }
}
