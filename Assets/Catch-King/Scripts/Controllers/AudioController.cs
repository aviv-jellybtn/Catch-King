using DG.Tweening;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoSingleton<AudioController>
{
    [SerializeField] private float _volume = 0.1f;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _charSelectionMusic;
    [SerializeField] private AudioClip _gameMusic;

    public AudioClip CHAR_SELECTED_SOUND;
    public AudioClip GAME_OVER_SOUND;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _volume;
    }

    public void PlayCharSelection()
    {
        _audioSource.clip = _charSelectionMusic;
    }

    public void PlayGame()
    {
        _audioSource.clip = _gameMusic;
        _audioSource.volume = 0f;
        _audioSource.Play();

        _audioSource.DOFade(_volume, 3f);
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip, 1f);
    }

}
