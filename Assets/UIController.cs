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
        Restart();

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
        // force scale to 1 because of fade bug
        target.transform.DOScale(1, 0); 

        var animDir = (AnimationDirection)Enum.Parse(typeof(AnimationDirection), direction.ToUpper());
        var startOpacity = isVisible ? Color.white : new Color(1, 1, 1, 0);
        var fade = target.DOFade(isVisible ? 0 : 1, animationDuration).ChangeStartValue(startOpacity);

        // only do left and right because im lazy
        if (animDir != AnimationDirection.LEFT && animDir != AnimationDirection.RIGHT)
        {
            sequence = DOTween.Sequence().Join(fade).OnStart(() => isVisible = !isVisible);
            return;
        }

        var dir = animDir == AnimationDirection.RIGHT ? -1 : 1;

        var targetPosX = startPos + (Vector3.right * 50) * dir;
        sequence = DOTween.Sequence().Join(
            target.transform.DOLocalMoveX(isVisible ? targetPosX.x : startPos.x, animationDuration).ChangeStartValue(isVisible ? startPos : targetPosX)
        ).Join(
            fade
        ).OnStart(() => isVisible = !isVisible);
    }

    public void Drop()
    {
        target.rectTransform.pivot = new Vector2(0.5f, 1);
        target.rectTransform.localPosition = Vector2.up * 350; // insane quick hack because im lazy
        target.DOFade(1, 0);

        sequence = DOTween.Sequence().Join(
            target.transform.DOScale(isVisible ? 0 : 1, animationDuration).ChangeStartValue(isVisible ? Vector3.one : Vector3.zero)
        ).OnStart(() => isVisible = !isVisible);
    }

    void Restart()
    {
        if (sequence.IsActive() && sequence.IsPlaying()) return;

        sequence.Kill();
        target.rectTransform.pivot = Vector2.one / 2;
        target.rectTransform.localPosition = Vector2.up * 100; // another quick pivot hack
        target.color = Color.white;
    }
}
