using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AvatarMovementController : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _velocity;

    public void AddVelocity(Vector3 velocity)
    {
        _velocity += velocity;
    }

    public void Move()
    {
        _characterController.Move(_velocity * Time.deltaTime);
        _velocity = Vector3.zero;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
}
