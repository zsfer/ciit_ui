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
    public float animSpeed = 5f;

    Sequence sequence;

    bool isVisible = false;

    private void Start()
    {
        sequence = DOTween.Sequence();
        DOTween.defaultAutoKill = false;
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

        sequence = DOTween.Sequence().Append(
            target.transform.DOScale(Vector3.one, animSpeed).ChangeStartValue(Vector3.zero)
        );
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

        sequence.PlayBackwards();

        return;
    }
}
