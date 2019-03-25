using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CameraPivot _pivot;

    [SerializeField]
    private float _distance;

    [SerializeField]
    private float _xAngle;

    [SerializeField]
    private float _yAngle;

    public Vector3 _direction;
	
	void Update () {
        UpdatePosition();
	}

    [ContextMenu("Find Pivot")]
    public void FindPivot()
    {
        _pivot = FindObjectOfType<CameraPivot>();
    }

    public void UpdatePosition()
    {
        _direction = Quaternion.Euler(_xAngle, _yAngle, 0) * Vector3.forward;
        
        _direction.Normalize();
        Vector3 pivotPosition;
        
        if(_pivot == null)
        {
            pivotPosition = Vector3.zero;
        }
        else
        {
            pivotPosition = _pivot.transform.position;
        }
      

        var deltaPos = -_direction * _distance;

        transform.position = Vector3.Lerp(transform.position, pivotPosition + deltaPos, 7 * Time.deltaTime);
        transform.forward = _direction;
    }
}
