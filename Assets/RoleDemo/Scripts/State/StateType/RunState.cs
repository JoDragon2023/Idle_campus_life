using System;

/// <summary>
/// 行走
/// </summary>
public class RunState : StateTemplate
{
    public RunState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Run, enterAct, exitAct, updateAct)
    {
    }
}
