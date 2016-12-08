using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    private PlayerController[] _playerControllers;

    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private RoundOver _roundOver;
    [SerializeField] private GameObject _menuUIHolder;
    [SerializeField] private GameObject _gameUIHolder;
    [SerializeField] private GameObject _gameHolder;

    private int _amountOfRunners;
    private int _runnersCaught;

    private int _readyPlayersCount;

    private bool _gameEnded;

    public void ChooseRandomCatcher(PlayerController[] playerContollers)
    {
        var choosenCatacherPlayerIndex = Random.Range(0, _playerControllers.Length);

        for (int playerIndex = 0; playerIndex < _playerControllers.Length; playerIndex++)
        {
            if (playerIndex == choosenCatacherPlayerIndex)
            {
                _playerControllers[playerIndex].SetPlayerType(PlayerType.Catcher);
                continue;
            }

            _playerControllers[playerIndex].SetPlayerType(PlayerType.Runner);
            _amountOfRunners++;
        }
    }

    public void IncrementReadyPlayer()
    {
        _readyPlayersCount++;
    }

    public void DeductReadyPlayer()
    {
        _readyPlayersCount--;
    }

    public void RunnerCaught()
    {
        _runnersCaught++;

        if (_runnersCaught == _amountOfRunners)
        {
            EndGame(true);
        }
    }

    public void RunnerReleased()
    {
        _runnersCaught--;
    }

    public void TryStartGame()
    {
        if (_readyPlayersCount < 2)
        {
            return;
        }

        HideLobby();
        StartGame();
    }

    private void HideLobby()
    {
        _menuUIHolder.SetActive(false);
    }

    private void ShowLobby()
    {
        _menuUIHolder.SetActive(true);
    }

    public void StartGame()
    {
        _gameUIHolder.SetActive(true);
        _gameHolder.SetActive(true);

        _gameEnded = false;
        _runnersCaught = 0;
        _amountOfRunners = 0;

        // Find all players
        _playerControllers = FindObjectsOfType<PlayerController>();
       
        // Set random catcher
        ChooseRandomCatcher(_playerControllers);

        // Start the round's timer
        _gameTimer.StartTimer();

        // Assign round ended event
        _gameTimer.RoundedEnded += EndGame;
    }

    public void EndGame(bool catcherWon)
    {
        if (_gameEnded)
        {
            return;
        }

        _gameEnded = true;
        _gameTimer.RoundedEnded -= EndGame;

        if (catcherWon)
        {
            _roundOver.ShowRoundOver(PlayerType.Catcher);
        }
        else
        {
            _roundOver.ShowRoundOver(PlayerType.Runner);
        }

        _gameTimer.StopTimer();
        Invoke("ReloadLevel", 5f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

}
