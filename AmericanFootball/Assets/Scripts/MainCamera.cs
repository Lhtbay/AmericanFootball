using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Game_Manager _gameManager;

    private GameObject _player;

    private Vector3 _targetDirection, _newDirection;

    private Camera _mainCamera;

    private float _targetSpeed = 0;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        _mainCamera = transform.GetComponent<Camera>();
    }

    private void Update()
    {
        _targetSpeed = _gameManager.CameraTargetRotationSpeed * Time.deltaTime;

        _targetDirection = _player.transform.position - transform.position;
        _newDirection = Vector3.RotateTowards(transform.forward, _targetDirection, _targetSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(_newDirection);

        _mainCamera.fieldOfView = Mathf.Lerp(
            _mainCamera.fieldOfView,
            _gameManager.FalseFOV,
            _gameManager.FalseFOVSmoothTime * Time.deltaTime);
    }
}
