using UnityEngine;
using DG.Tweening;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private float _loopDuration = 0.37f;

    private float _tiltPercentange;
    private int _direction = 1;

    [SerializeField] private Transform _loopAnimationTransform;
    [SerializeField] private Transform _jumpAnimationTransform;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Tweener _xScaleTween;
    private Tweener _yScaleTween;
    private Sequence _jumpTween;

    private void Start()
    {
        ActivateCharacterAnimation();
    }

    public void SetSpriteRenderer(Sprite newSprite)
    {
        if (_spriteRenderer == null)
        {
            return;
        }

        _spriteRenderer.sprite = newSprite;
    }

    public void ActivateCharacterAnimation()
    {
        _loopAnimationTransform.DOScaleX(0.95f, _loopDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        _loopAnimationTransform.DOScaleY(1.05f, _loopDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void JumpAnimation()
    {
        if (_jumpTween != null && _jumpTween.IsPlaying())
        {
            return;
        }

        _jumpTween = DOTween.Sequence();
        _jumpTween
            .Append(_jumpAnimationTransform.DOScaleY(1.3f, 0.18f).SetEase(Ease.OutSine))
            .Append(_jumpAnimationTransform.DOScaleY(1f, 0.18f).SetEase(Ease.InSine));
    }

    public void Tilt(int direction)
    {
        if (_tiltPercentange < 1)
        {
            _tiltPercentange += 0.05f;
        }

        _direction = direction;
    }

    private void Update()
    {
        var tiltValue = Mathf.Lerp(0f, 15f, _tiltPercentange) * _direction;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, tiltValue));
    }

    public void Untilt()
    {
        if (_tiltPercentange > 0)
        {
            _tiltPercentange -= 0.05f;
        }

        _direction = 1;
    }
}
