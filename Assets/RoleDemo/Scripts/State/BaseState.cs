using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    public RoleState state;
    
    public StateMachine machine;
    public BaseState(RoleState state)
    {
        this.state = state;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate(float deltaTime) { }
}
