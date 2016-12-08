using DG.Tweening;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    public bool CanSetFree;
    public bool IsFrozen;

    [SerializeField] private float _durationTillSetFree = 3f;

    private GameObject canSetFreeUI;
    private GameObject frozenUI;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }


    public void ActivateFrozen()
    {
        canSetFreeUI = Instantiate(Resources.Load("UI/SetFreeTimer_UI"), transform) as GameObject;
        canSetFreeUI.transform.localPosition = new Vector3(0, 1f, 0);
        var frozenSprite = canSetFreeUI.GetComponent<SpriteRenderer>();

        frozenUI = Instantiate(Resources.Load("UI/Frozen_UI"), transform) as GameObject;
        frozenUI.transform.localPosition = Vector3.zero;
           
        IsFrozen = true;
        CanSetFree = false;

        frozenSprite.DOFade(0f, _durationTillSetFree).SetEase(Ease.Linear).OnComplete(() => DoCanSetFree());
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_rigidbody.isKinematic && _collider.isTrigger)
        {
            return;
        }

        if (other.transform.position.y < transform.position.y && other.gameObject.tag == GameParameters.GROUND_TAG && IsFrozen)
        {
            _rigidbody.isKinematic = true;
            _collider.isTrigger = true;
        }
    }

    public void DisableFrozen()
    {
        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;

        Destroy(frozenUI);
        IsFrozen = false;

    }

    private void DoCanSetFree()
    {
        Destroy(canSetFreeUI);
        CanSetFree = true;
    }
}
