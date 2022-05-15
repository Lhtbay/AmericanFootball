using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image _distanceBarIMG;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _gameLostUI;
    [SerializeField] private GameObject _gameWinUI;

    [Header("Game Settings")]
    public bool GameStop = false;
    public bool GameLost = false;
    public bool GameWin = false;
    
    [Header("Enemy Settings")]
    public float Enemy_Speed;
    public int EnemySpawnCount;
    public float EnemySpawnTime1;
    public float EnemySpawnTime2;
    public float EnemySpawnTime3;
    public float EnemySpawnTime4;

    [Header("Friendly Settings")]
    public int Friendly_Speed;
    public int FriendlySpawnCount;
    public int Friendly_MinDistance;
    public int Friendly_MaxDistance;

    [Header("Map Settings")]
    public int Map_Count;

    [Header("Pooling Settings")]
    [SerializeField] private Enemy_Pooling _enemyPoolScript;
    [SerializeField] private Friendly_Pooling _friendlyPoolScript;

    [Header("Camera Settings")]
    public float CameraTargetRotationSpeed;
    public int FalseFOV;
    public float FalseFOVSmoothTime;

    private GameObject _touchDownLane, _player;
    private float _maxDistiance, _currentDistance;

    private void Start()
    {
        _touchDownLane = GameObject.FindGameObjectWithTag("Winner").gameObject;
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;

        _maxDistiance = Vector3.Distance(_touchDownLane.transform.position,_player.transform.position);

    }

    private void Update()
    {
        _currentDistance = Vector3.Distance(_touchDownLane.transform.position,_player.transform.position);
        _distanceBarIMG.fillAmount = _currentDistance / _maxDistiance ;       
    }

    private void FixedUpdate()
    {
        if (GameStop)
        {
            _enemyPoolScript.enabled = false;
            _friendlyPoolScript.enabled = false;
        }
        if (GameLost)
        {
            _gameUI.SetActive(false);
            _gameLostUI.SetActive(true);
        }
        if (GameWin)
        {
            _gameUI.SetActive(false);
            _gameWinUI.SetActive(true);
        }
    }

    #region BUTTONS

    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

}
