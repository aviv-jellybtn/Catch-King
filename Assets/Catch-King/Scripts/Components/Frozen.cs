using DG.Tweening;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    public bool CanSetFree;
    public bool IsFrozen;

    [SerializeField] private float _durationTillSetFree = 3f;
    [SerializeField] private AudioClip _frozenAudioClip;
    [SerializeField] private AudioClip _unfreezeAudipClip;

    private GameObject canSetFreeUI;
    private GameObject frozenUI;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private bool _shouldFrozen;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void Init(bool shouldFrozen)
    {
        _shouldFrozen = shouldFrozen;
    }

    public void ActivateFrozen()
    {
        if (!_shouldFrozen)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(_frozenAudioClip, transform.position);

        canSetFreeUI = Instantiate(Resources.Load("UI/SetFreeTimer_UI"), transform) as GameObject;
        canSetFreeUI.transform.localPosition = new Vector3(0f, 1f, 0f);
        var frozenSprite = canSetFreeUI.GetComponent<SpriteRenderer>();

        frozenUI = Instantiate(Resources.Load("UI/Frozen_UI"), transform) as GameObject;
        frozenUI.transform.localPosition = new Vector3(0f, 0.1f, 0f);
           
        IsFrozen = true;
        CanSetFree = false;

        frozenSprite.DOFade(0f, _durationTillSetFree).SetEase(Ease.Linear).OnComplete(() => DoCanSetFree());
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!_shouldFrozen)
        {
            return;
        }
        
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
        if (!_shouldFrozen)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(_unfreezeAudipClip, transform.position);

        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;

        Destroy(frozenUI);
        IsFrozen = false;

    }

    private void DoCanSetFree()
    {
        if (!_shouldFrozen)
        {
            return;
        }

        Destroy(canSetFreeUI);
        CanSetFree = true;
    }
}
