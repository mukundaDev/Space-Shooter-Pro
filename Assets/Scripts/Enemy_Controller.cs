using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class Enemy_Controller : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed = 4.0f;
    private Player_Behavior _player;
    private Animator _animator;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player_Behavior>();
       /* if(_player == null)
        {
            Debug.LogError("The Player is Null");
        }*/
        _animator = GetComponent<Animator>();
    /*if(_animator == null)
        {
            Debug.LogError("The animator is Null");
        }*/

    }

    // Updat is called once per frame
    void Update()
    {
       transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if(transform.position.y < -5f)
        {

            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            Player_Behavior player = other.GetComponent<Player_Behavior>();

            if(player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.6f);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.6f);
        }
    }
}
  