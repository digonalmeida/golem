using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdsBehaviour : MonoBehaviour
{
    public void ChangeState();
    public void OnStateEnter();
    public void OsStateLeave();
}

public abstract class AdsBehaviour : IAdsBehaviour
{
    private AdsBlock _owner;
    public abstract void ChangeState();
    public abstract void OnStateEnter();
    public abstract void OsStateLeave();
}

