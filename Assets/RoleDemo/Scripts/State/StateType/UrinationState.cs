using System;

//尿急
public class UrinationState : StateTemplate
{
    public UrinationState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Urination, enterAct, exitAct, updateAct)
    {
    }
}