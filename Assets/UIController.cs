using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationDirection
{
    NORMAL,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class UIController : MonoBehaviour
{
    public Image target;

    [Space]
    [Tooltip("Animation duration in seconds")]
    public float animationDuration = 5f;
    public Ease easingFunction = Ease.InOutCubic;

    Sequence sequence;

    bool isVisible = false;

    private void Start()
    {
        // Configure sequence stuff
        sequence = DOTween.Sequence();
        sequence.SetEase(easingFunction);
    }

    public void Scale()
    {
        if (!isVisible)
        {
            sequence = DOTween.Sequence()
            .Join(
                target.DOFade(1, animationDuration)
            ).Join(
                target.transform.DOScale(1, animationDuration).ChangeStartValue(Vector3.one * 0.9f)
            ).OnStart(() => isVisible = true);
        }
        else
        {
            sequence = DOTween.Sequence()
            .Join(
                target.DOFade(0, animationDuration)
            ).Join(
                target.transform.DOScale(0.9f, animationDuration)
            ).OnStart(() => isVisible = false);
        }
    }

    public void Zoom()
    {
        Restart();

        if (!isVisible)
        {
            sequence = DOTween.Sequence().Append(
                target.transform.DOScale(Vector3.one, animationDuration)
            ).OnStart(() => isVisible = true);
        }
        else
        {
            sequence = DOTween.Sequence().Append(
                target.transform.DOScale(Vector3.zero, animationDuration)
            ).OnStart(() => isVisible = false);
        }
    }

    public void Fade(AnimationDirection direction)
    {

    }

    public void Flip(bool isHorizontal)
    {

    }

    public void Drop()
    {

    }

    public void Fly(AnimationDirection direction)
    {

    }

    public void Swing(AnimationDirection direction)
    {

    }

    public void Browse(AnimationDirection direction)
    {

    }

    void Restart()
    {
        if (sequence.IsActive() && sequence.IsPlaying()) return;

        sequence.Kill();
        target.color = Color.white;
    }
}
