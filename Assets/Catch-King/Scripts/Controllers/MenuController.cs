using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Players Player;

    [SerializeField] private string _charSelectionRight;
    [SerializeField] private string _charSelectionLeft;
    [SerializeField] private string _charReadyButton;
    [SerializeField] private string _startButton;

    [SerializeField] private CharacterUI _characterUI;
    [SerializeField] private Text _readyText;

    private void Update()
    {
        if (Input.GetButtonDown(_charSelectionLeft) && !_readyText.enabled)
        {
            _characterUI.ChangeCharacter(false);
        }
        if (Input.GetButtonDown(_charSelectionRight) && !_readyText.enabled)
        {
            _characterUI.ChangeCharacter(true);
        }

        if (Input.GetButtonDown(_charReadyButton))
        {
            _readyText.enabled = !_readyText.enabled;

            if (_readyText.enabled)
            {
                AudioController.instance.PlayOneShot(AudioController.instance.CHAR_SELECTED_SOUND);
                GameController.instance.IncrementReadyPlayer(Player);
            }
            else
            {
                GameController.instance.DeductReadyPlayer(Player);
            }
        }

        if (Input.GetButtonDown(_startButton))
        {
            GameController.instance.TryStartGame();
        }
    }
}
