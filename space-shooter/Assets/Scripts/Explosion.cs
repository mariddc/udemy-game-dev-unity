using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _explosionAudioSource = GetComponent<AudioSource>();
        if (_explosionAudioSource == null)
        {
            Debug.LogError("! The explosion Audio Source is NULL.");
        }
        else 
        {
            _explosionAudioSource.Play();
        }
        
        Destroy(this.gameObject, 2.65f);
    }
}
