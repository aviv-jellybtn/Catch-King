using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventAnnouncerUI : MonoBehaviour
{
    private Text _eventAnnouncerText;

    private void Awake()
    {
        _eventAnnouncerText = GetComponent<Text>();
    }

    public void AnnounceEvent(string eventText)
    {
        _eventAnnouncerText.enabled = true;
        _eventAnnouncerText.text = eventText;

        var sequence = DOTween.Sequence();
        sequence
            .Append(_eventAnnouncerText.DOFade(1f, 1f))
            .Insert(3f, _eventAnnouncerText.DOFade(0f, 1f))
            .OnComplete(() => DisableEventAnnouncer());
           
    }

    private void DisableEventAnnouncer()
    {
        _eventAnnouncerText.text = string.Empty;
        _eventAnnouncerText.enabled = false;
    }
}
