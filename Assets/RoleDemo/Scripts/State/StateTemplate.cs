using System;

public class StateTemplate : BaseState
{
    public Action OnEnterAct;
    public Action OnExitAct;
    public Action<float> OnUpdateAct;
    
    public StateTemplate(RoleState state, Action enterAct, Action exitAct, Action<float> updateAct):base(state)
    {
        OnEnterAct = enterAct;
        OnExitAct = exitAct;
        OnUpdateAct = updateAct;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        OnEnterAct?.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        OnExitAct?.Invoke();
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        OnUpdateAct?.Invoke(deltaTime);
    }

    public void Release()
    {
        OnEnterAct = null;
        OnExitAct = null;
        OnUpdateAct = null;
    }
    
}
