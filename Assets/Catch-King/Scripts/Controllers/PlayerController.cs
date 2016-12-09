using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;
    // Condition for whether the player should jump.

    public float moveForce = 365f;
    // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;
    // The fastest the player can travel in the x axis.
    public float jumpForce = 1000f;
    // Amount of force added when the player jumps.
    public AudioClip[] jumpClips;
    // Array of clips for when the player jumps.

    private Transform groundCheck;
    // A position marking where to check if the player is grounded.
    private bool grounded = false;
    // Whether or not the player is grounded.

    // The player's type
    private PlayerType _playerType;

    private Frozen _frozen;
    private CharacterAnimation _characterAnimation;

    [SerializeField] private CharacterUI _characterUI;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    [SerializeField] private string _horizontalAxisName;
    [SerializeField] private string _jumpButtonName;
    [SerializeField] private ParticleSystem _catcherParticleSystem;

    private Rigidbody2D _rigidbody;

    public Players Player;

    private void Awake()
    {
        // Setting up references.
        _catcherParticleSystem.loop = false;
        groundCheck = transform.Find("groundCheck");

        _rigidbody = GetComponent<Rigidbody2D>();
        _frozen = GetComponent<Frozen>();
        _characterAnimation = GetComponentInChildren<CharacterAnimation>();
    }

    public void SetPlayerType(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.Catcher:
                {
                    moveForce *= GameParameters.CATCHER_MASS;
                    jumpForce *= GameParameters.CATCHER_MASS;
                    _rigidbody.mass = GameParameters.CATCHER_MASS;
                    maxSpeed = GameParameters.CATCHER_MAX_SPEED;
                    transform.localScale *= GameParameters.CATCHER_SIZE;
                    _catcherParticleSystem.loop = true;
                    gameObject.layer = GameParameters.CATCHER_LAYER;
                    Destroy(_frozen);
                    break;
                }
            case PlayerType.Runner:
                {
                    maxSpeed = GameParameters.RUNNER_MAX_SPEED;
                    transform.localScale *= GameParameters.RUNNER_SIZE;
                    gameObject.layer = GameParameters.RUNNER_LAYER;
                    break;
                }
            default:
                break;
        }

        _characterAnimation.SetSpriteRenderer(_characterUI.GetSprite());
        _playerType = playerType;
        gameObject.tag = _playerType.ToString();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        var frozen = other.gameObject.GetComponent<Frozen>();

        // Handle collision as catcher
        if (_playerType == PlayerType.Catcher)
        {
            if (other.gameObject.tag == GameParameters.RUNNER_TAG && frozen && !frozen.IsFrozen)
            {
                GameController.instance.RunnerCaught();
                other.gameObject.GetComponent<Frozen>().ActivateFrozen();
                other.gameObject.GetComponent<PlayerController>().enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var frozen = other.gameObject.GetComponent<Frozen>();
        if (_frozen.IsFrozen)
        {
            return;
        }

        // Handle collision as runner
        if (_playerType == PlayerType.Runner)
        {
            if (other.gameObject.tag == GameParameters.RUNNER_TAG)
            {
                if (other.gameObject.GetComponent<Frozen>() && frozen.CanSetFree && frozen.IsFrozen)
                {
                    GameController.instance.RunnerReleased();
                    other.gameObject.GetComponent<Frozen>().DisableFrozen();
                    other.gameObject.GetComponent<PlayerController>().enabled = true;
                }
            }
        }
    }

    //    private void OnCollisionEnter2D(Collision2D other)
    //    {
    //        // Handle collision as catcher
    //        if (_playerType == PlayerType.Catcher)
    //        {
    //            if (other.gameObject.tag == GameParameters.RUNNER_TAG && other.gameObject.GetComponent<Frozen>() && !other.gameObject.GetComponent<Frozen>().IsFrozen)
    //            {
    //                GameController.instance.RunnerCaught();
    //                other.gameObject.GetComponent<Frozen>().ActivateFrozen();
    //                other.gameObject.GetComponent<PlayerController>().enabled = false;
    //            }
    //        }
    //
    //        // Handle collision as runner
    //        if (_playerType == PlayerType.Runner)
    //        {
    //            if (other.gameObject.tag == GameParameters.RUNNER_TAG)
    //            {
    //                if (other.gameObject.GetComponent<Frozen>() && other.gameObject.GetComponent<Frozen>().CanSetFreeze)
    //                {
    //                    GameController.instance.RunnerReleased();
    //                    other.gameObject.GetComponent<Frozen>().DisableFrozen();
    //                    other.gameObject.GetComponent<PlayerController>().enabled = true;
    //                }
    //            }
    //        }
    //    }

    private void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
//        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown(_jumpButtonName) && grounded)
        {
            jump = true;
        }
    }


    private void FixedUpdate()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                grounded = true;
        }

        // Cache the horizontal input.
        float horizontalAxis = Input.GetAxis(_horizontalAxisName);

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (horizontalAxis * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalAxis * moveForce);
        }

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        {
            // ... set the player's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        // If the input is moving the player right and the player is facing left...
        if (horizontalAxis > 0.2f && !facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalAxis < -0.2f && facingRight)
        {
            Flip();
        }

        // If the player should jump...
        if (jump)
        {
            _characterAnimation.JumpAnimation();

            // Play a random jump audio clip.
            int i = Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
