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
    private bool _freeX;

    private void Start()
    {
        pivotPosition = transform.position;
        _movementController = GetComponent<AvatarMovementController>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRendererPoints = new Vector3[2];
    }

    public void Toggle()
    {
        enabled = !enabled;
    }

    public void ToggleFreeX()
    {
        _freeX = !_freeX;
    }
    
    private void Update()
    {
        var direction = pivotPosition - transform.position;
        if(_freeX)
        {
            direction.x = 0;
        }
        
        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            
            _movementController.AddVelocity(direction * force * Time.deltaTime);
        }

        _lineRendererPoints[0] = pivotPosition;
        if(_freeX)
        {
            _lineRendererPoints[0].x = transform.position.x;
        }
        _lineRendererPoints[1] = transform.position;
        _lineRenderer.SetPositions(_lineRendererPoints);
    }
}
