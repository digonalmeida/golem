using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
public class CharacterAnimationController : MonoBehaviour {
    [SerializeField]
    private Animator animator;

    CharacterMovementController movementController;

    private int idleAnimationParam;
    private int runningAnimationParam;
    private int knockedDownAnimationParam;
    private int punchAnimationParam;

    private void Awake()
    {
        movementController = GetComponent<CharacterMovementController>();
    }

    private void Start()
    {
        idleAnimationParam = Animator.StringToHash("idle");
        runningAnimationParam = Animator.StringToHash("running");
        knockedDownAnimationParam = Animator.StringToHash("knockedDown");
        punchAnimationParam = Animator.StringToHash("punch");
    }
	
	private void LateUpdate()
    {
        animator.SetBool(runningAnimationParam, movementController.MovementForce.sqrMagnitude > 0);
	}
}
