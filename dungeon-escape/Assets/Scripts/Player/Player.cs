using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int _diamonds = 0;
    private Rigidbody2D _body;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _grounded;
    private bool _restJump;
    private PlayerAnimation _animation;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArcSprite;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (_animation.GetStateInfo().IsName("Death"))
        {
            return;
        }

        Movement();

        //bool idle = _animation.GetStateInfo().IsName("Idle");
        if (Input.GetMouseButtonDown(0) && _grounded)
        {
            _animation.Attack();
        }
        
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        //flip
        if (horizontalInput < 0)
        {
            Flip(true);
        }
        else if (horizontalInput > 0)
        {
            Flip(false);
        }

        //move
        _body.velocity = new Vector2(horizontalInput * _speed, _body.velocity.y);
        _animation.Move(horizontalInput);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);
        if (hitInfo.collider != null && !_restJump)
        {
            _animation.Jump(false);
            return true;
        }
        return false;
    }

    void Flip(bool faceLeft)
    {
        //sprite
        _spriteRenderer.flipX = faceLeft;

        //sword arc
        Vector2 arcSpritePos = _swordArcSprite.transform.localPosition;
        _swordArcSprite.flipY = faceLeft;
        
        if ((faceLeft && arcSpritePos.x > 0) || (!faceLeft && arcSpritePos.x < 0))
        {
            arcSpritePos.x *= -1;
            _swordArcSprite.transform.localPosition = arcSpritePos;
        }
    }

    void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, _jumpForce);
        _animation.Jump(true);
        StartCoroutine(RestGroundCheck());
    }

    IEnumerator RestGroundCheck()
    {
        _restJump = true;
        yield return new WaitForSeconds(0.1f);
        _restJump = false;
    }

    public void Damage()
    {
        if (Health > 0)
        {
            Health--;
            UIManager.Instance.UpdateLives(Health);
            if (Health < 1)
            {
                _animation.Die();
            }
        }
        
    }

    public void UpdateDiamonds(int value)
    {
        _diamonds += value;
        UIManager.Instance.UpdateGemCount(_diamonds);
    }

    public int GetDiamonds()
    {
        return _diamonds;
    }

}
