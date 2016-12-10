using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

 public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private PlayerController[] _playerControllers;
    [SerializeField] private MenuController[] _menuControllers;
    [SerializeField] private GameObject[] _levels;

    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private RoundOver _roundOver;
    [SerializeField] private EventAnnouncerUI _eventAnnouncerUI;
    [SerializeField] private StartRoundUI _startRoundUI;
    [SerializeField] private EndGameUI _endGameUI;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _menuUIHolder;
    [SerializeField] private GameObject _gameUIHolder;
    [SerializeField] private GameObject _gameHolder;

    private List<PlayerController> _activePlayerControllers = new List<PlayerController>();
    private PlayerController _currentCatcherConroller;

    private int _amountOfRunners;
    private int _runnersCaught;

    private int _readyPlayersCount;

    private bool _gameEnded;

    public bool IsInverseControls;

    [SerializeField] private SpriteRenderer _nightSpriteRenderer;

    private int _roundCounter;
    [SerializeField] private int _roundsAmount = 3;

    private GameObject _currentLevel;

    public bool IsRoundRunning;

    public static event Action PlatformEventOccured;

    public void ChooseRandomCatcher(PlayerController[] playerContollers)
    {
        var choosenCatacherPlayerIndex = UnityEngine.Random.Range(0, _activePlayerControllers.Count);

        for (int playerIndex = 0; playerIndex < _activePlayerControllers.Count; playerIndex++)
        {
            if (playerIndex == choosenCatacherPlayerIndex)
            {
                _currentCatcherConroller = _activePlayerControllers[playerIndex];
                _activePlayerControllers[playerIndex].SetPlayerType(PlayerType.Catcher);
                continue;
            }

            _activePlayerControllers[playerIndex].SetPlayerType(PlayerType.Runner);
            _amountOfRunners++;
        }
    }

    private void ActivateRandomLevel()
    {
        var levelIndex = UnityEngine.Random.Range(0, _levels.Length);
        _currentLevel = _levels[levelIndex];
        _currentLevel.SetActive(true);
    }

    private void DisableLevel()
    {
        _currentLevel.SetActive(false);
    }

    public void IncrementReadyPlayer(Players player)
    {
        _readyPlayersCount++;

        foreach (var playerController in _playerControllers)
        {
            if (playerController.Player == player)
            {
                _activePlayerControllers.Add(playerController);
            }
        }

        if (_readyPlayersCount >= 2)
        {
            _startButton.gameObject.SetActive(true);
        }
    }

    public void DeductReadyPlayer(Players player)
    {
        _readyPlayersCount--;

        foreach (var playerController in _playerControllers)
        {
            if (playerController.Player == player)
            {
                _activePlayerControllers.Remove(playerController);
            }
        }

        if (_readyPlayersCount < 2)
        {
            _startButton.gameObject.SetActive(false);
        }
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
        PreRoundStart();
//        StartGame();
    }

    private void HideLobby()
    {
        _menuUIHolder.SetActive(false);
    }

    private void ShowLobby()
    {
        _menuUIHolder.SetActive(true);
    }

    private void InvokeWorldEvent()
    {
        var worldEventIndex = UnityEngine.Random.Range(0, 3);

        // Change platforms
        if (worldEventIndex == 0)
        {
            if (PlatformEventOccured != null)
            {
                PlatformEventOccured();
            }

            _eventAnnouncerUI.AnnounceEvent("NEW\nPLATFORMS\n!!!");
        }

        // NIGHT
        if (worldEventIndex == 1)
        {
            _nightSpriteRenderer.DOFade(1f, 1f).SetEase(Ease.Linear);

            _eventAnnouncerUI.AnnounceEvent("NIGHT\nMODE\n!!!");
        }

        // Inverse controls!!
        if (worldEventIndex == 2)
        {
            IsInverseControls = true;

            _eventAnnouncerUI.AnnounceEvent("INVERSE\nCONTROLS\n!!!");
        }
    }

    public void PreRoundStart()
    {
        _startRoundUI.ActivateStartRound();

        _gameUIHolder.SetActive(true);
        _gameHolder.SetActive(true);

        _gameEnded = false;
        _runnersCaught = 0;
        _amountOfRunners = 0;

        // Set random catcher
        ChooseRandomCatcher(_playerControllers);

        // Activate a random level
        ActivateRandomLevel();

        // Play game main theme
        AudioController.instance.PlayGame();

        // DISABLE CATCHER
        _currentCatcherConroller.enabled = false;
    }

    public void StartGame()
    {
        IsRoundRunning = true;

        // ENABLE CATCHER
        _currentCatcherConroller.enabled = true;

        // Start the round's timer
        _gameTimer.StartTimer();

        // Invoke a world event half way through the session
        Invoke("InvokeWorldEvent", _gameTimer.GetRoundTime() / UnityEngine.Random.Range(1.5f, 2.5f));

        // Assign round ended event
        _gameTimer.RoundedEnded += EndGame;
    }

    public void EndGame(bool catcherWon)
    {
        if (_gameEnded)
        {
            return;
        }

        _roundCounter++;

        CancelInvoke("InvokeWorldEvent");

        // Reset night mode
        _nightSpriteRenderer.color = new Color(0, 0, 0, 0);

        // Reset inverse controls
        IsInverseControls = false;

        IsRoundRunning = false;
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

        AudioController.instance.PlayOneShot(AudioController.instance.GAME_OVER_SOUND);

        _gameTimer.StopTimer();

        Invoke("ReloadLevel", 5f);
    }

    private void ReloadLevel()
    {
        if (_roundCounter <= _roundsAmount)
        {
            DisableLevel();

            // Reset players
            ResetActivePlayers();

            // HIDE GAME OVER UI
            _roundOver.DisableRoundOver();
    
            PreRoundStart();
        }
        else
        {
            // HIDE GAME OVER UI
            _roundOver.DisableRoundOver();
            _endGameUI.ShowEndGameLeaderboard();

            Invoke("ResetGame", 10f);
        
        }
    }

    private void ResetGame()
    {
        ScoreController.ResetScores();
        SceneManager.LoadScene(0);
    }

    private void ResetActivePlayers()
    {
        foreach (var activePlayerController in _activePlayerControllers)
        {
            activePlayerController.ResetPlayer();
        }
    }

}
