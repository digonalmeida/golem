using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBehaviour : MonoBehaviour
{
    [SerializeField] 
    private Transform _target;
    
    [SerializeField]
    private float _acceleration = 40.0f;

    [SerializeField] 
    private float _deacceleration = 5.0f;

    [SerializeField]
    private float _maxSpeed = 10;

    private Vector3 _velocity;

    private CharacterMovementController _characterMovementController;
    private CharacterAttackController _characterAttackController;
    private Vector3 _movementDirection;
    private Vector3[] _linePositions = new Vector3[2];
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _characterMovementController = GetComponent<CharacterMovementController>();
        _characterAttackController = GetComponent<CharacterAttackController>();
        _characterAttackController.OnHandInstantiated += OnHandInstantiated;
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDestroy()
    {
        _characterAttackController.OnHandInstantiated -= OnHandInstantiated;
    }

    private void Update()
    {
        var acceleration = _acceleration;
        if (_target == null || !Input.GetButton("Fire2"))
        {
            _movementDirection = Vector3.zero;
            acceleration = _deacceleration;
            _lineRenderer.enabled = false;
        }
        else
        {
            _movementDirection = _target.position - transform.position;
            if (_movementDirection.sqrMagnitude != 0)
            {
                _movementDirection.Normalize();
            }

            _characterMovementController.IgnoreGravityThisFrame = true;

            _lineRenderer.enabled = true;
        }
        
        
        _velocity = Vector3.MoveTowards(_velocity, _movementDirection * _maxSpeed, acceleration * Time.deltaTime);
        _characterMovementController.Move(_velocity);
        
    }

    private void LateUpdate()
    {
        if (_lineRenderer.enabled && _target != null)
        {
            _linePositions[0] = transform.position;
            _linePositions[1] = _target.position;
            _lineRenderer.SetPositions(_linePositions);
        }
    }

    private void OnHandInstantiated(HandBehaviour handInstance)
    {
        _target = handInstance.transform;
    }
}
