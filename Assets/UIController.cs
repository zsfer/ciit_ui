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

    [Tooltip("Animation duration in seconds")]
    public float animSpeed = 5f;
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
        float opacity = 0;

        if (!isVisible)
        {
            sequence = DOTween.Sequence()
            .Join(
                DOTween.To(() => opacity, t => opacity = t, 1, animSpeed).OnUpdate(() =>
                {
                    target.color = new Color(1, 1, 1, opacity);
                })
            ).Join(
                target.transform.DOScale(1, animSpeed).ChangeStartValue(Vector3.one * 0.9f)
            ).OnStart(() => isVisible = true);
        }
        else
        {
            sequence = DOTween.Sequence()
                .Join(
                    DOTween.To(() => opacity, t => opacity = t, 0, animSpeed).ChangeStartValue(1).OnUpdate(() =>
                    {
                        target.color = new Color(1, 1, 1, opacity);
                    })
                ).Join(
                    target.transform.DOScale(0.9f, animSpeed).ChangeStartValue(Vector3.one)
                ).OnStart(() => isVisible = false);
        }
    }

    public void Zoom()
    {
        Restart();

        if (!isVisible)
        {
            sequence = DOTween.Sequence().Append(
                target.transform.DOScale(Vector3.one, animSpeed).ChangeStartValue(Vector3.zero)
            ).OnStart(() => isVisible = true);
        }
        else
        {
            sequence = DOTween.Sequence().Append(
                target.transform.DOScale(Vector3.zero, animSpeed).ChangeStartValue(Vector3.one)
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
