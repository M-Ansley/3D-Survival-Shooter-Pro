using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Objects")]
    private CharacterController _controller;
    private GameObject _cameraObj;

    [Header("Movement")]
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveVelocity = Vector3.zero;
    [SerializeField] private float _movementSpeed = 5f;


    [Header("Jumping and Gravity")]
    [SerializeField] private float _jumpHeight = 25f;
    private float _gravity = 1;
    private float _yVelocity = 0;

    [Header("Controller Settings")]

    [Range(0.1f, 3f)]
    [SerializeField] private float _mouseSensitivity = 1f;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cameraObj = Camera.main.gameObject;
        SetCursorLock(true);
    }

    private void Update()
    {
        PlayerInput();
        CameraLook();
    }
    
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _yVelocity = 0;
            if (_controller.isGrounded)
            {
                _yVelocity = _jumpHeight;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(false);
        }
    }

    private void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        Vector3 newPlayerRotation = transform.localEulerAngles;
        newPlayerRotation.y += mouseX * _mouseSensitivity;
        transform.localRotation = Quaternion.AngleAxis(newPlayerRotation.y, Vector3.up);
        //Vector3 newRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX * _mouseSensitivity, transform.localEulerAngles.z);
         //transform.localEulerAngles = newRotation;

        Vector3 newCameraRotation = _cameraObj.transform.localEulerAngles;
        newCameraRotation.x -= mouseY * _mouseSensitivity;
        _cameraObj.transform.localRotation = Quaternion.AngleAxis(newCameraRotation.x, Vector3.right);


    }

    private void FixedUpdate()
    {
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        float horizontalVal = Input.GetAxis("Horizontal");
        float verticalVal = Input.GetAxis("Vertical");
        _moveDirection = new Vector3(horizontalVal, 0, verticalVal);
        _moveVelocity = _moveDirection * _movementSpeed;

        if (!_controller.isGrounded)
        {
            _yVelocity -= _gravity;
        }

        _moveVelocity.y = _yVelocity;
        _moveVelocity = transform.TransformDirection(_moveVelocity); // converts it from world space to local space

        _controller.Move(_moveVelocity * Time.deltaTime);
    }

    private void SetCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
