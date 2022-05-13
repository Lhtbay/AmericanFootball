using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Game_Manager _gameManager;

    private GameObject _player;
    private GameObject _poolEnemy;

    private float _timer;

    private bool _timeToFalse = false;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _poolEnemy = GameObject.FindGameObjectWithTag("PoolEnemy").gameObject;
    }

    void Update()
    {
        transform.Translate((Vector3.forward*-1) * Time.deltaTime * _gameManager.Enemy_Speed);
       
        if (_timeToFalse)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1.5f)
            {              
                _timer = 0;
                _timeToFalse=false;
                this.gameObject.SetActive(false);
                transform.parent = _poolEnemy.transform;
            }
        }
        if (Vector3.Distance(_player.transform.position, transform.position) < 5)
        {
            _timeToFalse = true;
        }
    }
}
