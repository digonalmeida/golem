using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    private Vector3 direction;

	void Start ()
    {
		
	}
	
	private void Update ()
    {
        Debug.DrawRay(transform.position, direction, Color.red, 1f);
	}

    //private bool IsColliding()
    //{
    //    //if (Physics.Raycast(transform.position, direction, Color.red, 1f)){

    //    //}
    //} 

    public void SetDirection(Vector3 d)
    {
        direction = d;
    }
}
