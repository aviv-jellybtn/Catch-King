using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{
    [SerializeField] private bool _isRight;

    private void Awake()
    {
        PositionBorder(_isRight);
    }

    private void PositionBorder(bool isRight)
    {
        var stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

        if (_isRight)
        {
            transform.localPosition = new Vector3(stageDimensions.x +0.5f, -2, 0);
        }
        else
        {
            transform.localPosition = new Vector3(-stageDimensions.x - 0.5f, -2, 0);

        }
    }

}
