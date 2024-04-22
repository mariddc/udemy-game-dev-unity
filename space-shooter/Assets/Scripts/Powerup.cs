using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _powerupID; // 0 = Triple Shot, 1 = Speed, 2 = Shield

    [SerializeField] AudioClip _powerupAudio;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0: 
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.BoostSpeed();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default: 
                        Debug.Log("Default Value");
                        break;
                }
            }

            AudioSource.PlayClipAtPoint(_powerupAudio, transform.position);
            Destroy(this.gameObject);
        }
    }
}
