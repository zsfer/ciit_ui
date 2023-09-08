using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
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

    Vector3 startPos;

    private void Start()
    {
        sequence = DOTween.Sequence();
        sequence.SetEase(easingFunction);

        startPos = target.transform.localPosition;
    }

    public void Scale()
    {
        sequence = DOTween.Sequence()
            .Join(
                target.DOFade(isVisible ? 0 : 1, animationDuration)
            ).Join(
                target.transform.DOScale(isVisible ? 0.9f : 1, animationDuration).ChangeStartValue(isVisible ? Vector3.one : Vector3.one * 0.9f)
            ).OnStart(() => isVisible = !isVisible);
    }

    public void Zoom()
    {
        Restart();

        sequence = DOTween.Sequence().Append(
            target.transform.DOScale(isVisible ? Vector3.zero : Vector3.one, animationDuration)
        ).OnStart(() => isVisible = !isVisible);
    }

    public void Fade(string direction)
    {
        Restart();

        var animDir = Enum.Parse(typeof(AnimationDirection), direction.ToUpper());
        var startOpacity = isVisible ? Color.white : new Color(1, 1, 1, 0);
        var fade = target.DOFade(isVisible ? 0 : 1, animationDuration).ChangeStartValue(startOpacity);

        // TODO rewrite this to be shorter,
        // combine left/right in 1 case
        // combine up/down in 1 case

        switch (animDir)
        {
            case AnimationDirection.RIGHT:
                var targetPosX = startPos - (Vector3.right * 50);
                sequence = DOTween.Sequence().Join(
                    target.transform.DOLocalMoveX(isVisible ? targetPosX.x : startPos.x, animationDuration).ChangeStartValue(isVisible ? startPos : targetPosX)
                ).Join(
                    fade
                ).OnStart(() => isVisible = !isVisible);
                break;
            case AnimationDirection.LEFT:
                targetPosX = startPos + (Vector3.right * 50);
                sequence = DOTween.Sequence().Join(
                    target.transform.DOLocalMoveX(isVisible ? targetPosX.x : startPos.x, animationDuration).ChangeStartValue(isVisible ? startPos : targetPosX)
                ).Join(
                    fade
                ).OnStart(() => isVisible = !isVisible);
                break;
            case AnimationDirection.NORMAL:
            default:
                sequence = DOTween.Sequence().Join(fade).OnStart(() => isVisible = !isVisible);
                break;
        }

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
