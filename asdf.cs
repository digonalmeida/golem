using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    Layers playerLayer = Layers.Player;
    public float timeToFall = .5f;
    float timeCounter = 0;
    float gravitySpeed = -9.8f;
    bool falling = false;

	private void Start ()
    {
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (falling == false)
            return;

        timeCounter += Time.deltaTime;

        if (timeCounter < timeToFall)
            return;

        Fall();
    }

    private void Fall()
    {
        transform.position += new Vector3(0, gravitySpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        var layer = collision.gameObject.GetComponent<ObjectInfo>().layer;
        if (layer == playerLayer)
        {
            falling = true;
        }
    }
}
