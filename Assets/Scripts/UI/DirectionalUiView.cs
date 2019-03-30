using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalUiView : MonoBehaviour
{
    [SerializeField] 
    private GameObject _icon;
    
    [SerializeField] 
    private float _clampDistance = 30.0f;
    
    public void Show(Vector2 origin, Vector2 destination)
    {
        gameObject.SetActive(true);
        transform.position = origin;
        
        _icon.transform.position = GetClampedDestination(origin, destination);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private Vector2 GetClampedDestination(Vector2 origin, Vector2 destination)
    {
        var distanceVector = destination - origin;
        if (distanceVector.sqrMagnitude == 0)
        {
            return distanceVector;
        }
        
        distanceVector = Vector2.ClampMagnitude(distanceVector, _clampDistance);
        return origin + distanceVector;
    }
}
