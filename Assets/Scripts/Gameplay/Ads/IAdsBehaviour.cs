using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdsBehaviour
{
    void ChangeState();
    void OnStateEnter();
    void OnStateLeave();
}

public abstract class AdsBehaviour : MonoBehaviour, IAdsBehaviour 
{
    private AdsBlock _owner;
    public abstract void ChangeState();
    public abstract void OnStateEnter();
    public abstract void OnStateLeave();
}

