using UnityEngine;
using UnityEngine.UI;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private float _roundTime = 240f; // In seconds

    private float _timer;
    private bool _running;

    public event Action<bool> RoundedEnded;

    public float GetRoundTime()
    {
        return _roundTime;
    }

    public void StartTimer()
    {
        gameObject.SetActive(true);
        _timer = _roundTime;
        _running = true;
    }

    public void StopTimer()
    {
        _running = false;
    }

    void Update()
    {
        if (!_running)
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            if (RoundedEnded != null)
            {
                RoundedEnded(false);
            }
            return;
        }

        var minutes = Mathf.Floor(_timer / 60f); //Divide the guiTime by sixty to get the minutes.
        var seconds = Mathf.Clamp( _timer % 60, 0, 59); //Use the euclidean division for the seconds.

        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
