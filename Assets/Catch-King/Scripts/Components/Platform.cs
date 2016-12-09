using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    private PlatformType _type;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        InitPlatform();
        GameController.PlatformEventOccured += OnWorldEventOccured;
    }

    private void OnDestroy()
    {
        GameController.PlatformEventOccured -= OnWorldEventOccured;
    }

    private void InitPlatform()
    {
        var tileMaterial = new Material(SpriteController.instance.TileableSpriteShader);
        _spriteRenderer.material = tileMaterial;
        _spriteRenderer.material.SetFloat("RepeatX", transform.localScale.x);

        SetPlaform(PlatformType.Normal);
    }

    private void OnWorldEventOccured()
    {
        var platformType = (PlatformType)Random.Range(0, 3);
        SetPlaform(platformType);
    }

    public void SetPlaform(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Normal:
                {
                    _spriteRenderer.sprite = SpriteController.instance.NormalPlatformSprite;
                    _boxCollider.sharedMaterial = SpriteController.instance.NormalPlatformMaterial;
                    break;
                }
            case PlatformType.Rough:
                {
                    _spriteRenderer.sprite = SpriteController.instance.RoughPlatformSprite;
                    _boxCollider.sharedMaterial = SpriteController.instance.RoughPlatformMaterial;
                    break;
                }
            case PlatformType.Slippery:
                {
                    _spriteRenderer.sprite = SpriteController.instance.SlipperyPlatformSprite;
                    _boxCollider.sharedMaterial = SpriteController.instance.SlipperyPlatformMaterial;
                    break;
                }
            default:
                break;
        }
    }
}
