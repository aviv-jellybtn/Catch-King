using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoundOver : MonoBehaviour
{
    [SerializeField] private Text _roundOverText;

    public void ShowRoundOver(PlayerType winnerType)
    {
        gameObject.SetActive(true);
        _roundOverText.enabled = true;

        var winnerName = winnerType.ToString();
        if (winnerType == PlayerType.Runner)
        {
            winnerName += "s";
        }

        var text = string.Format("Round Over! \n{0} Won!", winnerName);
        _roundOverText.text = text;

        _roundOverText.DOFade(1f, 1f);
    }

    public void DisableRoundOver()
    {
        _roundOverText.enabled = false;
        _roundOverText.color = new Color(1, 1, 1, 0);
    }
}   
