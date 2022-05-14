using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _playerNormalSpeed;
    [SerializeField] private float _playerRuningSpeed;
    [SerializeField] private float _normalXLocation;
    [SerializeField] private float _runningXLocation;
    [SerializeField] private float _leftPassTime;
    [SerializeField] private float _anglePassTime;
    [SerializeField] private float _returnNormalizedTime;

    [Header("Camera Settings")]
    [SerializeField] private int _normalFOV;
    [SerializeField] private int _runingsFOV;
    [SerializeField] private float _smoothTime;
    private Camera _mainCamera;

    private bool _stop,_winner = false;

    private float _playerSpeed;
    private float _newXPosition;
    private float _timer,_winnerTimer;

    private int _randomWinAnimationCount;

    private Transform _normalZRotation;
    private Transform _runningZRotation;
    private Transform _backNormalRotation;
    private Transform _playerTransform;

    private GameObject _playerGameObject;

    private Animator _playerAnim;

    private Game_Manager _gameManager;

    private List<Rigidbody> _ragdollRB;
    private List<Collider> _ragdollCollider;

    private void Start()
    {
        _ragdollRB = new List<Rigidbody>();
        _ragdollCollider = new List<Collider>();

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _playerGameObject = GameObject.FindGameObjectWithTag("Player").gameObject;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        _normalZRotation = GameObject.FindGameObjectWithTag("NorRot").gameObject.transform;
        _runningZRotation = GameObject.FindGameObjectWithTag("RunRot").gameObject.transform;
        _backNormalRotation = GameObject.FindGameObjectWithTag("BackRot").gameObject.transform;

        _playerAnim = _playerTransform.GetComponent<Animator>();

        _ragdollRB.AddRange(_playerGameObject.GetComponentsInChildren<Rigidbody>());
        _ragdollCollider.AddRange(_playerGameObject.GetComponentsInChildren<Collider>());

        foreach (var item in _ragdollRB)
        {
            item.isKinematic = true;
        }
        foreach (var item in _ragdollCollider)
        {           
            item.enabled = false;
        }

        _timer = _returnNormalizedTime;
    }

    private void Update()
    {
        if (_winner)
        {
            _winnerTimer += Time.deltaTime;
            if (_winnerTimer>=0.5f)
            {
                _winnerTimer = 0;
                _mainCamera.GetComponent<MainCamera>().enabled = true;
                _stop = true;
            }
        }
        if (!_stop)
        {
            PlayerController();

            transform.Translate(Vector3.forward * Time.deltaTime * _playerSpeed);
        }
        else
        {                    
            _playerAnim.SetTrigger("win"+_randomWinAnimationCount);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Winner")
        {           
            _gameManager.GameStop = true;
            _randomWinAnimationCount = Random.Range(1, 4);
            _winner = true;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            _stop = true;
            _mainCamera.GetComponent<MainCamera>().enabled = true;
            _gameManager.GameStop = true;
            RagdolSysthem();
        }
    }   

    #region METODS

    private void PlayerController()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _timer = 0;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            _timer = 0;
        }
        if (Input.GetButton("Fire1"))
        {
            _playerSpeed = _playerRuningSpeed;

            _newXPosition = Mathf.Lerp(transform.position.x, _runningXLocation, _leftPassTime*Time.deltaTime);
            
            transform.position = new Vector3(
                _newXPosition,
                transform.position.y,
                transform.position.z);

           
            _timer += Time.deltaTime;

            if (_timer >= _returnNormalizedTime)
            {
                _playerTransform.rotation = Quaternion.Lerp(
                _playerTransform.rotation,
                _normalZRotation.rotation,
                _anglePassTime * Time.deltaTime);
            }
            else
            {
                _playerTransform.rotation = Quaternion.Lerp(
                _playerTransform.rotation,
                _runningZRotation.rotation,
                _anglePassTime * Time.deltaTime);
            }

            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _runingsFOV, _smoothTime* Time.deltaTime);

        }
        else
        {
            _playerSpeed = _playerNormalSpeed;

            _newXPosition = Mathf.Lerp(transform.position.x, _normalXLocation, _leftPassTime * Time.deltaTime);

            transform.position = new Vector3(
               _newXPosition,
               transform.position.y,
               transform.position.z);
           
            _timer += Time.deltaTime;

            if (_timer >= _returnNormalizedTime)
            {
                _playerTransform.rotation = Quaternion.Lerp(
                _playerTransform.rotation,
                _normalZRotation.rotation,
                _anglePassTime * Time.deltaTime);
            }
            else
            {
                _playerTransform.rotation = Quaternion.Lerp(
                _playerTransform.rotation,
                _backNormalRotation.rotation,
                _anglePassTime * Time.deltaTime);
            }

            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _normalFOV, _smoothTime * Time.deltaTime);
        }
    }

    private void RagdolSysthem()
    {
        _playerAnim.enabled = false;

        foreach (var item in _ragdollCollider)
        {
            item.enabled = true;
        }
        foreach (var item in _ragdollRB)
        {
            item.isKinematic = false;
        }
       
    }

    #endregion

}
