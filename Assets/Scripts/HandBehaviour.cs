using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandStates
{
    Going,
    Waiting,
    Backing
}

public class HandBehaviour : MonoBehaviour
{

    [SerializeField]
    private float backSpeed = 15f;
    [SerializeField]
    private float speed = 10f;

    public HandStates currentState { get; set; }
    private CharacterAttackController player;
    private Vector3 startPosition;
    private Vector3 distanceTravelled;
    private float maxDistance = 10f;
    private Vector3 direction;
    private HandCollider handCollider;

    public void Initialize(ref CharacterAttackController p)
    {
        player = p;
        startPosition = player.transform.position;
        currentState = HandStates.Going;
        direction = player.transform.forward;
    }

    private void Start()
    {
        handCollider = GetComponent<HandCollider>();
    }
    private void Update()
    {
        switch (currentState)
        {
            case HandStates.Going:
                GoFoward();
                break;
            case HandStates.Waiting:
                Wait();
                break;
            case HandStates.Backing:
                GoBack();
                break;
        }
    }

    private void GoFoward()
    {
        Vector3 deltaPos = direction * speed * Time.deltaTime;

        transform.position += deltaPos;
        deltaPos = deltaPos.normalized;
        DirectionToCollider(ref deltaPos);
        if (Vector3.Distance(startPosition, transform.position) >= 10)
        {
            currentState = HandStates.Waiting;
        }
    }

    private void Wait()
    {

    }

    private void GoBack()
    {
        Vector3 oldPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.handPosition.position, backSpeed * Time.deltaTime);
        Vector3 dir = (transform.position - oldPosition).normalized;
        DirectionToCollider(ref dir);
        if (Vector3.Distance(transform.position, player.handPosition.position) < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetState(HandStates state)
    {
        currentState = state;
    }

    private void DirectionToCollider(ref Vector3 d)
    {
        handCollider.SetDirection(d);
    }

}
