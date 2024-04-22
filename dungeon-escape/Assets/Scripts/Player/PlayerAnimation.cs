using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Animator _swordAnim;

    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponentInChildren<Animator>();
        //_swordAnim = transform.GetChild(1).GetComponent<Animator>();

        Animator[] animators = GetComponentsInChildren<Animator>();
        for (int i = 0; i < animators.Length; i++)
        {
            switch (animators[i].name)
            {
                case "Sprite":
                    _animator = animators[i];
                    break;
                case "Sword_Arc":
                    _swordAnim = animators[i];
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(float moveInput)
    {
        _animator.SetFloat("Move", Mathf.Abs(moveInput));
    }

    public void Jump(bool jumpInput)
    {
        _animator.SetBool("Jumping", jumpInput);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");
    }

    public void Die()
    {
        _animator.SetTrigger("Death");
    }

    public AnimatorStateInfo GetStateInfo()
    {
        return _animator.GetCurrentAnimatorStateInfo(0);
    }
}
