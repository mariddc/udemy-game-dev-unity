using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy{

    //initialize
    public override void Init()
    {
        base.Init();
    }

    public override void Damage()
    {
        Debug.Log("Skeleton:: Damage()");
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);

        base.Damage();
    }

}
