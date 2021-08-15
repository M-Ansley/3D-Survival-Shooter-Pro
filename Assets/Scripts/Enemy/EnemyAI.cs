using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable<float>, IKillable
{
    private CharacterController _controller;
    private Player _player;

    [Header("Movement")]
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveVelocity = Vector3.zero;
    [SerializeField] private float _movementSpeed = 1f;

    [Header("Jumping and Gravity")]
    [SerializeField] private float _jumpHeight = 25f;
    private float _gravity = 1;
    private float _yVelocity = 0;

    [Header("Rotation")]
    private float _rotateSpeed = 3f;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = FindObjectOfType<Player>();
    }
    
    void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (_controller.isGrounded)
        {
            _moveDirection = _player.gameObject.transform.position - transform.position;
            _moveDirection.y = 0;
            _moveVelocity = _moveDirection * _movementSpeed;
        }
        else
        {
            _yVelocity -= _gravity;
        }

        RotateToPlayer();
        _moveVelocity.y = _yVelocity;
        _controller.Move(_moveVelocity * Time.deltaTime);
    }

    private void RotateToPlayer()
    {
        float step = _rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, _moveDirection, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }


    #region Health_and_Damage

    public void Damage(float damageTaken) // From IDamageable Interface
    {

    }

    public void Kill() // From IKillable Interface
    {

    }

    #endregion
}
