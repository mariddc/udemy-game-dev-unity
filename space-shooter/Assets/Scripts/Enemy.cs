using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _yInitial = 8.0f;
    private Player _player;
    private Animator _animator;

    private AudioSource _explosionSound;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("! The Player is NULL.");
        }

        _animator = GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("! The Animator is NULL.");
        }

        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null) 
        {
            Debug.LogError("! The enemy Audio Source is NULL.");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -6.0f)
        {
            //float xInitial = Random.value * 9.35f * 2 - 9.35f;
            float xInitial = Random.Range(-9f, 9f);
            transform.position = new Vector3(xInitial, _yInitial, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.tag == "Player")
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            //null check
            if (player != null)
            {
                player.Damage();
            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _explosionSound.Play();
            Destroy(this.gameObject, 2.7f);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.UpdateScore(10);                
            }
          
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _explosionSound.Play();
            Destroy(this.gameObject, 2.7f);
        }
    }
}
