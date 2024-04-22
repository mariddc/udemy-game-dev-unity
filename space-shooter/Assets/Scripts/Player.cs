using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _speedMultiplier = 2;
    [SerializeField] private GameObject _laserPrefab, _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFire = -1f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _tripleShotOn = false;
    private bool _speedBoostOn = false;
    private bool _shieldOn = false;
    [SerializeField] private GameObject _shieldVisual;
    [SerializeField] private int _score = 0;
    private UIManager _uiManager;

    [SerializeField] private GameObject _rightEngineHurt, _leftEngineHurt;
    [SerializeField] private AudioClip _laserShotAudio;
    private AudioSource _playerAudioSource;
        
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerAudioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("! The Spawn Manager is NULL.");
        }
        if(_uiManager == null)
        {
            Debug.LogError("! The UI Manager is NULL.");
        }

        if(_playerAudioSource == null)
        {
            Debug.LogError("! The player Audio Source is NULL.");
        }
        else
        {
            _playerAudioSource.clip = _laserShotAudio;
        }

    }
     
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float realSpeed;

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_speedBoostOn)
        {
            realSpeed = _speed * _speedMultiplier;
        }
        else
        {
            realSpeed = _speed;
        }

        transform.Translate( direction * realSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), transform.position.z);


        if (transform.position.x <= -11.27f)
        {
            transform.position = new Vector3(11.27f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 11.27f)
        {
            transform.position = new Vector3(-11.27f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        if (_tripleShotOn)
        {
            Vector3 tripleLaserOffset = new Vector3(0.025f, 0.3f, 0);
            Instantiate(_tripleShotPrefab, transform.position + tripleLaserOffset, transform.rotation);
        }
        else
        {
            Vector3 laserOffset = new Vector3(0, 1.05f, 0);
            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
        } 

        _playerAudioSource.Play();

    }

    public void Damage()
    {
        if (_shieldOn)
        {
            _shieldOn = false;
            _shieldVisual.SetActive(false);
            return;
        }

        

        _lives -= 1;
        UpdateScore(-10);
        _uiManager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _rightEngineHurt.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngineHurt.SetActive(true);
        }
        else if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverSequence();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _tripleShotOn = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _tripleShotOn = false;
    }

    public void BoostSpeed()
    {
        _speedBoostOn = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speedBoostOn = false;
    }

    public void ActivateShield()
    {
        _shieldOn = true;
        _shieldVisual.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _shieldOn = false;
        _shieldVisual.SetActive(false);
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
