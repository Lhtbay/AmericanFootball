using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pooling : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPref;
    private GameObject _player;

    private Game_Manager _gameManager;

    private List<GameObject> _enemyList;

    private float _timer;

    private int _spawnTime=2;

    private bool _timeStop = false;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _enemyList = new List<GameObject>();

        for (int i = 0; i <= _gameManager.EnemySpawnCount; i++)
        {
            GameObject newEnemy = Instantiate(_enemyPref, transform.position, Quaternion.identity);

            newEnemy.transform.parent = transform;
            newEnemy.SetActive(false);

            _enemyList.Add(newEnemy);
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,_player.transform.position.z+25);

        _timer += Time.deltaTime;
        if (_timer >= _spawnTime)
        {
            Spawner();
        }

    }

    private void Spawner()
    {
        
        foreach (var item in _enemyList)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.parent = null;
                item.SetActive(true);
                _timer = 0;
                _spawnTime = Random.Range(1,3);               
                break;
            }          
        }
    }

}
