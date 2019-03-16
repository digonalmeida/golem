using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterForce
{
    Vector3 velocity = Vector3.zero;
}

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private float movementAcceleration = 40.0f;

    [SerializeField]
    private float movementReverseAccelerationMultiplier = 1.5f;

    [SerializeField]
    private float maxJumpForce = 20.0f;

    [SerializeField]
    private float minJumpForce = 8.0f;

    [SerializeField]
    public Vector3 movementDirection = Vector3.zero;

    [SerializeField]
    private float movementMaxSqrSpeed = 10.0f;

    [SerializeField]
    private float gravityAcceleration = 40.0f;

    [SerializeField]
    private float gravityMaxSqrSpeed = 30.0f;

    [SerializeField]
    private float gravitySqrMagnitudeCanjumpTreshold = 50.0f;

    [SerializeField]
    private float turningRateRad = 12.0f;

    private bool canJump = true;
    public bool IgnoreGravityThisFrame = false;
    private CharacterController characterController;
    private Vector3 movementForce = Vector3.zero;
    private Vector3 gravityForce = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 externalForce = Vector3.zero;
    

    public void Move(Vector3 velocity)
    {
        externalForce += velocity;
    }
    

    public Vector3 MovementForce
    {
        get
        {
            return movementForce;
        }
    }
    
 
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

   
    
    private void Update()
    {
        UpdateMovementForce();
        UpdateGravityForce();
        
        velocity = movementForce + gravityForce + externalForce;
        Vector3 motion = velocity * Time.deltaTime;
        characterController.Move(motion);

        if(movementDirection.sqrMagnitude > 0)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, movementDirection.normalized, turningRateRad * Time.deltaTime, 1);
        }

        externalForce = Vector3.zero;
    }

    private void UpdateMovementForce()
    {
        if(movementDirection.sqrMagnitude > 1)
        {
            movementDirection.Normalize();
        }

        float acceleration = movementAcceleration;
        if(movementForce.sqrMagnitude > 0)
        {
            if (Vector3.Angle(movementForce.normalized, movementDirection) > 90)
            {
                acceleration *= movementReverseAccelerationMultiplier;
            }
        }
        
        movementForce = Vector3.MoveTowards(movementForce, movementDirection * movementMaxSqrSpeed, acceleration * Time.deltaTime);
    }

    private void UpdateGravityForce()
    {
        if (IgnoreGravityThisFrame)
        {
            canJump = false;
            gravityForce = Vector3.zero;
            IgnoreGravityThisFrame = false;
            return;
        }
        if(characterController.isGrounded && gravityForce.y <= 0)
        {
            gravityForce = Vector3.zero;
            canJump = true;
        }

        gravityForce = Vector3.MoveTowards(gravityForce, Physics.gravity * gravityMaxSqrSpeed, gravityAcceleration * Time.deltaTime);
        if(gravityForce.sqrMagnitude > gravitySqrMagnitudeCanjumpTreshold)
        {
            canJump = false;
        }
    }

    public void StartJump()
    {
        if(!canJump)
        {
            return;
        }

        gravityForce = -Physics.gravity.normalized * maxJumpForce;
        canJump = false;
    }

    public void StopJump()
    {
        gravityForce = Vector3.Min(-Physics.gravity.normalized * minJumpForce, gravityForce);
    }
}
