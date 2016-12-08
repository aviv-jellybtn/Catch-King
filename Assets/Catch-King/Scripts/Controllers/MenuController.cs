using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string _charSelectionRight;
    [SerializeField] private string _charSelectionLeft;
    [SerializeField] private string _charReadyButton;
    [SerializeField] private string _startButton;

    [SerializeField] private CharacterUI _characterUI;
    [SerializeField] private Text _readyText;

    private void Update()
    {
        if (Input.GetButtonDown(_charSelectionLeft))
        {
            _characterUI.ChangeCharacter(false);
        }
        if (Input.GetButtonDown(_charSelectionRight))
        {
            _characterUI.ChangeCharacter(true);
        }

        if (Input.GetButtonDown(_charReadyButton))
        {
            _readyText.enabled = !_readyText.enabled;

            if (_readyText.enabled)
            {
                GameController.instance.IncrementReadyPlayer();
            }
            else
            {
                GameController.instance.DeductReadyPlayer();
            }
        }

        if (Input.GetButtonDown(_startButton))
        {
            GameController.instance.TryStartGame();
        }
    }
}
