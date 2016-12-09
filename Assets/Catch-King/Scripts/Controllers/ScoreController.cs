using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ScoreController
{
    private static Dictionary<Players,int> _playersScoreDictionary;

    public static event Action<Players, int> ScoreUpdated;

    static ScoreController()
    {
        _playersScoreDictionary = new Dictionary<Players, int>();
        _playersScoreDictionary.Add(Players.Player1, 0);
        _playersScoreDictionary.Add(Players.Player2, 0);
        _playersScoreDictionary.Add(Players.Player3, 0);
        _playersScoreDictionary.Add(Players.Player4, 0);
    }

    public static void ResetScores()
    {
        _playersScoreDictionary[Players.Player1] = 0;
        _playersScoreDictionary[Players.Player2] = 0;
        _playersScoreDictionary[Players.Player3] = 0;
        _playersScoreDictionary[Players.Player4] = 0;
    }

    public static int GetScore(Players player)
    {
       return _playersScoreDictionary[player];
    }

    public static void UpdateScore(Players player, bool add)
    {
        if (add)
        {
            _playersScoreDictionary[player]++;
        }
        else
        {
            if (_playersScoreDictionary[player] != 0)
            {
                _playersScoreDictionary[player]--;
            }
        }

        if (ScoreUpdated != null)
        {
            ScoreUpdated(player, _playersScoreDictionary[player]);
        }
    }

}
