using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private int _amountOfRounds = 3;

    private int _roundsCounter;

    public bool ShouldEndGame;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void CountRound()
    {
        _roundsCounter++;

        if (_roundsCounter >= _amountOfRounds)
        {
            ShouldEndGame = true;
        }
    }

    public void Reset()
    {
        _roundsCounter = 0;
        ShouldEndGame = false;
    }
}
