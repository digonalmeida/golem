using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedPivotController : MonoBehaviour
{
    private Vector3 pivotPosition;
    private float maxDistance = 10;
    [SerializeField] 
    private float force;

    private LineRenderer _lineRenderer;

    private AvatarMovementController _movementController;
    private Vector3[] _lineRendererPoints;

    private void Start()
    {
        pivotPosition = transform.position;
        _movementController = GetComponent<AvatarMovementController>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRendererPoints = new Vector3[2];
    }
    
    private void Update()
    {
        var direction = pivotPosition - transform.position;
        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            _movementController.AddVelocity((pivotPosition - transform.position).normalized * force * Time.deltaTime);
        }

        _lineRendererPoints[0] = pivotPosition;
        _lineRendererPoints[1] = transform.position;
        _lineRenderer.SetPositions(_lineRendererPoints);
    }
}
