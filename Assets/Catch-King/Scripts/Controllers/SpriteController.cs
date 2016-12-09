using UnityEngine;
using System.Collections;

public class SpriteController : MonoSingleton<SpriteController>
{   
    public Sprite NormalPlatformSprite;
    public Sprite SlipperyPlatformSprite;
    public Sprite RoughPlatformSprite;

    public PhysicsMaterial2D NormalPlatformMaterial;
    public PhysicsMaterial2D SlipperyPlatformMaterial;
    public PhysicsMaterial2D RoughPlatformMaterial;

    public Shader TileableSpriteShader;
}
