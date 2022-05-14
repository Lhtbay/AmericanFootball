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

    private float _spawnTime = 0.2f;
    private int _spawnRandom;

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
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                _enemyList[i].transform.position = new Vector3(-1, 0, _player.transform.position.z + 15);
                _enemyList[i].transform.parent = null;
                _enemyList[i].SetActive(true);
            }
            else
            {
                _enemyList[i].transform.position = new Vector3(-1, 0, _player.transform.position.z + (15*(i+1)));
                _enemyList[i].transform.parent = null;
                _enemyList[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,_player.transform.position.z+40);

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
                _spawnRandom = Random.Range(1,5);
                
                switch (_spawnRandom)
                {
                    case 1:
                        _spawnTime = _gameManager.EnemySpawnTime1;
                     break;
                    case 2:
                        _spawnTime = _gameManager.EnemySpawnTime2;
                        break;
                    case 3:
                        _spawnTime = _gameManager.EnemySpawnTime3;
                        break;
                    case 4:
                        _spawnTime = _gameManager.EnemySpawnTime4;
                        break;

                }
                break;
            }          
        }
    }

}
