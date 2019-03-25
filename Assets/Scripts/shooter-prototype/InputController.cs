using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 _direction;

    public bool IsDirectionZero
    {
        get
        {
            return _direction.sqrMagnitude < 0.3f;
        }
    }

    public Vector3 Direction
    {
        get
        {
            return _direction;
        }
    }

    private void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
    }
}
