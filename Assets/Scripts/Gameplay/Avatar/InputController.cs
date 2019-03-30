using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] 
    private DirectionalUiView _directionalUi;

    
    private Vector3 _direction;
    private Vector3 _origin;
    private Vector3 _destination;

    public bool IsDirectionZero
    {
        get
        {
            return _direction.sqrMagnitude < 0.01f;
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
        UpdateMobileInput();
        if(IsDirectionZero)
        {
            UpdateStandalone();
        }
    }

    private void UpdateStandalone()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        _direction.y = 0;
    }

    private void UpdateMobileInput()
    {
        _direction = Vector3.zero;

        if(!Input.GetMouseButton(0))
        {
            _directionalUi.Hide();
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            _origin = Input.mousePosition;
        }
        _destination = Input.mousePosition;
        _direction = (_destination - _origin);
        if(_direction.sqrMagnitude > 0)
        {
            _direction.Normalize();
        }

        _direction.z = _direction.y;
        _direction.y = 0;

        _directionalUi.Show(_origin, _destination);
    }
}
