using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour {
    [SerializeField]
    public CameraPivot pivot;

    [SerializeField]
    public float distance;

    [SerializeField]
    public float xAngle;

    [SerializeField]
    public float yAngle;

    public Vector3 direction;
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
	}

    public void UpdatePosition()
    {
        direction = Quaternion.Euler(xAngle, yAngle, 0) * Vector3.forward;
        
        direction.Normalize();
        Vector3 pivotPosition;
        if(pivot == null)
        {
            pivotPosition = Vector3.zero;
        }
        else
        {
            pivotPosition = pivot.transform.position;
        }

        var deltaPos = -direction * distance;

        transform.position = Vector3.Lerp(transform.position, pivotPosition + deltaPos, 0.5f);
        transform.forward = direction;

    }
}
