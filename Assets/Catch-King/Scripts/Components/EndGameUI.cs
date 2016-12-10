using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Color P1_Color;
    [SerializeField] private Color P2_Color;
    [SerializeField] private Color P3_Color;
    [SerializeField] private Color P4_Color;

    [SerializeField] private Text _firstPlaceText;
    [SerializeField] private Text _secondPlaceText;
    [SerializeField] private Text _thirdPlaceText;
    [SerializeField] private Text _fourthPlaceText;

    [SerializeField] private GameObject _endGameConffetti;

    private bool _p1Set;
    private bool _p2Set;
    private bool _p3Set;
    private bool _p4Set;

    public void ShowEndGameLeaderboard()
    {
        gameObject.SetActive(true);
        _endGameConffetti.SetActive(true);

        var p1Score = ScoreController.GetScore(Players.Player1);
        var p2Score = ScoreController.GetScore(Players.Player2);
        var p3Score = ScoreController.GetScore(Players.Player3);
        var p4Score = ScoreController.GetScore(Players.Player4);

        var scores = new List<int>();
        scores.Add(p1Score);
        scores.Add(p2Score);
        scores.Add(p3Score);
        scores.Add(p4Score);

        scores.Sort();
        scores.Reverse();

            
        // SET FIRST PLACE
        if (scores[0] == p1Score && !_p1Set)
        {
            _firstPlaceText.text = "P1 " + scores[0].ToString();
            _firstPlaceText.color = P1_Color;
            _p1Set = true;
        }
        else if (scores[0] == p2Score && !_p2Set)
        {
            _firstPlaceText.text = "P2 " + scores[0].ToString();
            _firstPlaceText.color = P2_Color;
            _p2Set = true;
        }
        else if (scores[0] == p3Score && !_p3Set)
        {
            _firstPlaceText.text = "P3 " + scores[0].ToString();
            _firstPlaceText.color = P3_Color;
            _p3Set = true;
        }
        else if (scores[0] == p4Score && !_p4Set)
        {
            _firstPlaceText.text = "P4 " + scores[0].ToString();
            _firstPlaceText.color = P4_Color;
            _p4Set = true;
        }


        // SET SECOND PLACE
        if (scores[1] == p1Score && !_p1Set)
        {
            _secondPlaceText.text = "P1 " + scores[1].ToString();
            _secondPlaceText.color = P1_Color;
            _p1Set = true;
        }
        else if (scores[1] == p2Score && !_p2Set)
        {
            _secondPlaceText.text = "P2 " + scores[1].ToString();
            _secondPlaceText.color = P2_Color;
            _p2Set = true;
        }
        else if (scores[1] == p3Score && !_p3Set)
        {
            _secondPlaceText.text = "P3 " + scores[1].ToString();
            _secondPlaceText.color = P3_Color;
            _p3Set = true;
        }
        else if (scores[1] == p4Score && !_p4Set)
        {
            _secondPlaceText.text = "P4 " + scores[1].ToString();
            _secondPlaceText.color = P4_Color;
            _p4Set = true;
        }


        // SET THIRD PLACE
        if (scores[2] == p1Score && !_p1Set)
        {
            _thirdPlaceText.text = "P1 " + scores[2].ToString();
            _thirdPlaceText.color = P1_Color;
            _p1Set = true;
        }
        else if (scores[2] == p2Score && !_p2Set)
        {
            _thirdPlaceText.text = "P2 " + scores[2].ToString();
            _thirdPlaceText.color = P2_Color;
            _p2Set = true;
        }
        else if (scores[2] == p3Score && !_p3Set)
        {
            _thirdPlaceText.text = "P3 " + scores[2].ToString();
            _thirdPlaceText.color = P3_Color;
            _p3Set = true;
        }
        else if (scores[2] == p4Score && !_p4Set)
        {
            _thirdPlaceText.text = "P4 " + scores[2].ToString();;
            _thirdPlaceText.color = P4_Color;
            _p4Set = true;
        }


        // SET FIRST PLACE
        if (scores[3] == p1Score && !_p1Set)
        {
            _fourthPlaceText.text = "P1 " + scores[3].ToString();
            _fourthPlaceText.color = P1_Color;
            _p1Set = true;
        }
        else if (scores[3] == p2Score && !_p2Set)
        {
            _fourthPlaceText.text = "P2 " + scores[3].ToString();
            _fourthPlaceText.color = P2_Color;
            _p2Set = true;
        }
        else if (scores[3] == p3Score && !_p3Set)
        {
            _fourthPlaceText.text = "P3 " + scores[3].ToString();
            _fourthPlaceText.color = P3_Color;
            _p3Set = true;
        }
        else if (scores[3] == p4Score && !_p4Set)
        {
            _fourthPlaceText.text = "P4 " + scores[3].ToString();
            _fourthPlaceText.color = P4_Color;
            _p4Set = true;
        }
    }
}
