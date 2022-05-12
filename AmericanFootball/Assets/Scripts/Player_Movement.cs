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
    private Transform _normalZRotation;
    private Transform _runningZRotation;
    private Transform _backNormalRotation;
    private Transform _playerTransform;
    private Animator _playerAnim;

    [Header("Camera Settings")]
    [SerializeField] private int _normalFOV;
    [SerializeField] private int _runingsFOV;
    [SerializeField] private float _smoothTime;
    private Camera _mainCamera;

    private float _playerSpeed;
    private float _newXPosition;
    private float _timer;

    private int _randomWinAnimationCount;

    private bool _stop = false;

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _playerAnim = _playerTransform.GetComponent<Animator>();

        _normalZRotation = GameObject.FindGameObjectWithTag("NorRot").gameObject.transform;
        _runningZRotation = GameObject.FindGameObjectWithTag("RunRot").gameObject.transform;
        _backNormalRotation = GameObject.FindGameObjectWithTag("BackRot").gameObject.transform;

        _timer = _returnNormalizedTime;
    }

    private void Update()
    {
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
            _stop = true;
            _randomWinAnimationCount = Random.Range(1, 4);
        }
        else if (other.gameObject.tag == "Enemy")
        {

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

    #endregion

}
