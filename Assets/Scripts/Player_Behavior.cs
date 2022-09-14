using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Player_Behavior : MonoBehaviour
{
    [SerializeField]
    private int _speed;
    [SerializeField]
    private GameObject _laserBeam;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _canFire =  -1f;
    private int _lives = 3;
    [SerializeField]
    private bool _isTripleShotEnable = false;
   [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioSource _audioSource;
    private int _score;     
    private Spawn_manager _spawnManager;

    private UI_Controller _uiManager;


    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Controller>();
        _audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (_audioSource == null)
        {

        }
        else
        {
            _audioSource.clip = _laserSound;
        }
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.5)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }


        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.deltaTime + _fireRate;

        if (_isTripleShotEnable == true)
        { 
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserBeam, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        
        _lives--;


        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotEnable = true;
        StartCoroutine(TripleShotDownRoutine());

    }

    IEnumerator TripleShotDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotEnable = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
  