using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null && _canDamage)
        {
            hit.Damage();
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        _canDamage = false;
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
