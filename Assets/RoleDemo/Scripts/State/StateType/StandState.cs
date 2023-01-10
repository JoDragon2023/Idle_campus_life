using System;

/// <summary>
/// 站立
/// </summary>
public class StandState : StateTemplate
{
    public StandState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Stand, enterAct, exitAct, updateAct)
    {
    }
}