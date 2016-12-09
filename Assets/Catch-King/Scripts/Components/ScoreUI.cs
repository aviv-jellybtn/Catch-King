using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Players _player;
    [SerializeField] private string _playerName;
    private Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
        ScoreController.ScoreUpdated += OnScoreUpdated;
        UpdateScore(ScoreController.GetScore(_player));
    }

    private void OnDestroy()
    {
        ScoreController.ScoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreUpdated(Players player, int score)
    {
        if (_player == player)
        {
            UpdateScore(score);
        }
    }

    public void UpdateScore(int score)
    {
        var scoreText = string.Format("{0}: {1}", _playerName, score);
        _scoreText.text = scoreText;
    }
}
