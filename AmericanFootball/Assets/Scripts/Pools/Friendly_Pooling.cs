using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly_Pooling : MonoBehaviour
{
    [SerializeField] private GameObject _friendlyPref;
    [SerializeField] private GameObject _friendlyParent;
    private GameObject _player;

    private Game_Manager _gameManager;

    private List<GameObject> _friendlyList;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _friendlyList = new List<GameObject>();

        for (int i = 0; i <= _gameManager.FriendlySpawnCount; i++)
        {
            GameObject newFriendly = Instantiate(_friendlyPref, transform.position, Quaternion.identity);

            newFriendly.transform.parent = _friendlyParent.transform;           

            _friendlyList.Add(newFriendly);
        }

        for (int i = 0; i < _friendlyList.Count; i++)
        {
            int randomdistance = Random.Range(_gameManager.Friendly_MinDistance, _gameManager.Friendly_MaxDistance);
            if (i == 0 )
            {
                _friendlyList[i].transform.localPosition = transform.position -
                                new Vector3(transform.position.x,
                                transform.position.y,
                                _player.transform.position.z + randomdistance);

                if (_friendlyList[i].transform.position.z < 0)
                {
                    _friendlyList[i].transform.position = new Vector3 (
                        _friendlyList[i].transform.position.x,
                        _friendlyList[i].transform.position.y,
                        _friendlyList[i].transform.position.z*-1);
                }


                _friendlyList[i].SetActive(true);
            }
            else
            {
                _friendlyList[i].transform.localPosition = transform.position -
                new Vector3(transform.position.x,
                transform.position.y,
                _player.transform.position.z + (randomdistance * (i+1)));

                if (_friendlyList[i].transform.position.z < 0)
                {
                    _friendlyList[i].transform.position = new Vector3(
                        _friendlyList[i].transform.position.x,
                        _friendlyList[i].transform.position.y,
                        _friendlyList[i].transform.position.z * -1);
                }

                _friendlyList[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,_player.transform.position.z+25);      
    }

    private void Spawner()
    {
        
        foreach (var item in _friendlyList)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.parent = null;
                item.SetActive(true);                                           
                break;
            }          
        }
    }

}
