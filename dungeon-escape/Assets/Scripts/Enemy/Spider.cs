using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField] private AcidEffect _acidPrefab;

    //initialize
    public override void Init()
    {
        base.Init();
        //_acid = GameObject.FindGameObjectWithTag("Acid").GetComponent<AcidEffect>();
    }

    public override void Update()
    {
        
    }

    public override void Damage()
    {
        base.Damage();
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position - new Vector3(0.7f,0,0), Quaternion.identity);
    }

}
