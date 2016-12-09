using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _characterSprites;

    private Image _charImage;
    private int _currentCharIndex;

    private void Awake()
    {
        _charImage = GetComponent<Image>();
    }

    private void Start()
    {
        _currentCharIndex = Random.Range(0, _characterSprites.Length);
        _charImage.sprite = _characterSprites[_currentCharIndex];
    }

    public Sprite GetSprite()
    {
        return _charImage.sprite;
    }

    public void ChangeCharacter(bool positive)
    {
        if (positive)
        {
            _currentCharIndex++;  
            if (_currentCharIndex > _characterSprites.Length - 1)
            {
                _currentCharIndex = 0;
            }
        }
        else
        {
            _currentCharIndex--;
            if (_currentCharIndex < 0)
            {
                _currentCharIndex = _characterSprites.Length - 1;
            }
        }

        _charImage.sprite = _characterSprites[_currentCharIndex];
    }

}
