using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoundOver : MonoBehaviour
{
    [SerializeField] private Text _roundOverText;

    public void ShowRoundOver(PlayerType winnerType)
    {
        gameObject.SetActive(true);

        var text = string.Format("Round Over! \n {0} Won!", winnerType.ToString());
        _roundOverText.text = text;

        _roundOverText.DOFade(1f, 1f);
    }
}   
