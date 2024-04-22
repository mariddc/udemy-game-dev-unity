using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;

    private void Start()
    {
        Destroy(this.gameObject, 5.0f); //delay
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            IDamageable hit = other.collider.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.Damage();
            }
            Destroy(this.gameObject);
        }
    }

 
}
