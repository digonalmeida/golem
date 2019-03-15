using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
public class CharacterMovementInput : MonoBehaviour {
    CharacterMovementController movementController;
	
    private void Awake()
    {
        movementController = GetComponent<CharacterMovementController>();
    }

	void Update ()
    {
        var forward = Vector3.Cross(Camera.main.transform.right, Vector3.up);
        movementController.movementDirection = Camera.main.transform.right * Input.GetAxis("Horizontal");
        movementController.movementDirection += forward * Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            movementController.StartJump();
            Debug.Log("here");
        }
        else if (Input.GetButtonUp("Jump"))
        {
            movementController.StopJump();
        }
    }
}
