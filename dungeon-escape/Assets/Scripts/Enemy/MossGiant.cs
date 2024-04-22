using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{

    //initialize
    public override void Init()
    {
        base.Init();
    }

    public override void Damage()
    {
        Debug.Log("MossGiant:: Damage()");
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);

        base.Damage();
    }

}
