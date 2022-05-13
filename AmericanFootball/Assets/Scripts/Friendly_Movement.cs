using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly_Movement : MonoBehaviour
{
    [SerializeField] private GameObject _uniformNumbersParent;

    private Game_Manager _gameManager;

    private List<GameObject> _uniformNumberList;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();

        _uniformNumberList = new List<GameObject>();

        for (int i = 0; i < _uniformNumbersParent.transform.childCount; i++)
        {
            _uniformNumberList.Add(_uniformNumbersParent.transform.GetChild(i).gameObject);
        }
        _uniformNumberList[Random.Range(0, _uniformNumberList.Count)].SetActive(true);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _gameManager.Friendly_Speed);
    }

}
