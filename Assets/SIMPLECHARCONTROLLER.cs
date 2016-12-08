using UnityEngine;

public class SIMPLECHARCONTROLLER : MonoBehaviour
{

    [SerializeField] private string _horizontalAxisName;
    [SerializeField] private string _jumpButtonName;

    private void Update()
    {
        var horizontalInputDirection = Input.GetAxis(_horizontalAxisName);
        transform.position += new Vector3(horizontalInputDirection, 0, 0);

        var isPressingJump = Input.GetButton(_jumpButtonName);
        var sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = !isPressingJump;
    }
}
