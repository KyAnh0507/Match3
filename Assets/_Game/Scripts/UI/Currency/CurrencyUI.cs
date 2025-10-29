using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private Transform iconTf;
    [SerializeField] private TMP_Text amountTMP;
    public Canvas canvas;

    private int oldValue;
    private Transform cachedTransform;
    private Tweener valueTweener;
    private Tweener scaleTweener;
    private Tweener colorTweener;

    public Transform IconTf => iconTf;

    private void Awake()
    {
        cachedTransform = transform;
    }

    public void SetActiveOverrideSorting(bool isActive)
    {
        canvas.overrideSorting = isActive;
    }

    public void ChangeValue(int newValue, float duration = 1f, UnityAction OnComplete = null)
    {
        CompleteAllTween();

        if (Mathf.Approximately(duration, 0f))
        {
            oldValue = newValue;
            amountTMP.text = newValue.ToString();
        }
        else
        {
            valueTweener = DOVirtual.Int(oldValue, newValue, duration, value => amountTMP.text = value.ToString());
            valueTweener.OnComplete(() =>
            {
                oldValue = newValue;
                OnComplete?.Invoke();
            });
        }
    }

    public void ChangeValueScale(int newValue, float duration = 1f, float scale = 1.2f, UnityAction OnComplete = null)
    {
        ChangeValue(newValue, duration, OnComplete);
        ChangeScale(scale, duration);
    }

    public void ChangeValueColor(int newValue, float duration = 1f, UnityAction OnComplete = null)
    {
        ChangeValue(newValue, duration, OnComplete);
        ChangeColor(newValue > oldValue, duration);
    }

    public void ChangeValueScaleColor(int newValue, float duration = 1f, float scale = 1.2f, UnityAction OnComplete = null)
    {
        ChangeValue(newValue, duration, OnComplete);
        ChangeScale(scale, duration);
        ChangeColor(newValue > oldValue, duration);
    }

    public void ChangeValueImmediately(int value)
    {
        CompleteAllTween();

        oldValue = value;
        amountTMP.text = value.ToString();
    }

    protected virtual void ChangeScale(float scale, float duration)
    {
        scaleTweener = cachedTransform.DOScale(scale, duration);
    }

    protected virtual void ChangeColor(bool isIncrease, float duration)
    {
        Color color = isIncrease ? Color.green : Color.red;
        colorTweener = amountTMP.DOColor(color, duration);
    }

    protected virtual void CompleteAllTween()
    {
        CompleteTween(valueTweener);
        CompleteTween(scaleTweener);
        CompleteTween(colorTweener);
    }

    protected void CompleteTween(Tweener tweener)
    {
        if (tweener.IsActive())
        {
            tweener.Complete();
        }
    }
}
