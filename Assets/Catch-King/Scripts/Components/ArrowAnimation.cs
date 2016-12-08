using DG.Tweening;
using UnityEngine;
using System.Collections;

public class ArrowAnimation : MonoBehaviour
{
    [SerializeField] private bool _positiveX;

    private  void Start()
    {
        var yValue = _positiveX ? 5f : -5f;

        transform.DOLocalMoveY(transform.localPosition.y + yValue, 0.18f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
  
}
