using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartRoundUI : MonoBehaviour
{
    [SerializeField] private string[] _text;

    private Text _startRoundText;

    private const float _tickTime  = 1f;

    private void Awake()
    {
        _startRoundText = GetComponent<Text>();
        _startRoundText.enabled = false;  
    }

    public void ActivateStartRound()
    {
        _startRoundText.text = string.Empty;
        _startRoundText.color = new Color(1, 1, 1, 1);
        _startRoundText.enabled = true;
        StartCoroutine(ShowText());
    }
  
    private IEnumerator ShowText()
    {
        var i = 0;
        while (i < _text.Length)
        {
            yield return new WaitForSeconds(_tickTime);
            _startRoundText.text += _text[i];
            i++;
        }

        _startRoundText.DOFade(0f, 1f).SetDelay(1f).OnComplete(() => StartRound());
        yield return null;
    }

    private void StartRound()
    {
        GameController.instance.StartGame();
    }

}
