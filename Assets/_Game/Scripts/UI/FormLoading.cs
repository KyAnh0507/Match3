using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class FormLoading : MonoBehaviour
{
    public Image background;
    public float loadDuration;
    public Text text;
    public Transform fillImage;
    public AnimationCurve loadingCurve;

    private void Start()
    {
        background.sprite = GameConfig.Ins.themeGames[DataManager.Ins.dataSaved.theme].sprites[0];

        DOVirtual.Float(0f, 1f, loadDuration, value =>
        {
            fillImage.localScale = new Vector3(value, 1, 1);
            text.text = (int)(value*100) + "%";
        })
        .SetEase(loadingCurve)
        .OnComplete(() =>
        {
            UIManager.Ins.ChangeScene(Scene.Home);
        });
    }
}
