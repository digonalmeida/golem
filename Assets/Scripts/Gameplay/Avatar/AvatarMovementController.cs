using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AvatarMovementController : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _velocity;
    [SerializeField]
    private float _deascelerationSpeed = 1.0f;


    public void AddVelocity(Vector3 velocity)
    {
        _velocity += velocity;
        _velocity.y = 0;
    }

    public void Move()
    {
        _characterController.Move(_velocity * Time.deltaTime);
        _velocity = _characterController.velocity;
        _velocity = Vector3.Lerp(_velocity, Vector3.zero, _deascelerationSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
}
