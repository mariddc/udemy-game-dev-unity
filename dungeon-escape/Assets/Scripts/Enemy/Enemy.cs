using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int diamonds;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 target;
    protected Animator animator;
    protected SpriteRenderer sprite;

    private Player player;

    protected bool isHit = false;

    [SerializeField] protected Diamond diamondPrefab;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        Health = health;

        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("! The Player Script is NULL.");
        }

        //animator.SetBool("InCombat", false);

    }

    public virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Movement();
        }

        CheckAndCombat();

    }


    public virtual void Movement()
    {
        if (target == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (target == pointB.position)
        {
            sprite.flipX = false;
        }

        if (transform.position.x == pointA.position.x)
        {
            target = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position.x == pointB.transform.position.x)
        {
            target = pointA.position;
            animator.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public virtual void CheckAndCombat()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (isHit && distanceToPlayer > 2.5f)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }

        if (animator.GetBool("InCombat"))
        {
            Vector3 relativePosToPlayer = player.transform.position - transform.position;
            if (relativePosToPlayer.x > 0) sprite.flipX = false;
            else sprite.flipX = true;
        }
    }

    public virtual void Damage()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) 
            return;

        health -= 1;
        if (health < 1)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        animator.SetTrigger("Death");
        Diamond diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
        diamond.SetValue(diamonds);

    }
}
