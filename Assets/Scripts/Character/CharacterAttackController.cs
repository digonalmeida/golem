using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{

    [SerializeField]
    private bool isAttacking;
    private HandBehaviour instantiatedHand;
    public GameObject hand;
    public Transform handPosition;

    public event Action<HandBehaviour> OnHandInstantiated;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Attack();

	}

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (instantiatedHand == null)
            {
                PunchAttack();
            }
            else if (instantiatedHand.currentState == HandStates.Going)
            {
                instantiatedHand.SetState(HandStates.Waiting);
            }
            else if (instantiatedHand.currentState == HandStates.Waiting)
            {
                instantiatedHand.SetState(HandStates.Backing);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
        }
    }

    private void PunchAttack()
    {
        isAttacking = true;
        GameObject go  = Instantiate(hand, handPosition.position, Quaternion.identity);
        instantiatedHand = go.GetComponent<HandBehaviour>();
        CharacterAttackController cA = this;
        instantiatedHand.Initialize(ref cA);
        NotifyHandInstantiated(instantiatedHand);
    }

    private void NotifyHandInstantiated(HandBehaviour handBehaviour)
    {
        if (OnHandInstantiated != null)
        {
            OnHandInstantiated.Invoke(handBehaviour);
        }
    }
    
}
