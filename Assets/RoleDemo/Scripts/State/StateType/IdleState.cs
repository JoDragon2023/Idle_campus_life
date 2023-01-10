using System;

/// <summary>
/// 休闲状态
/// </summary>
public class IdleState:StateTemplate
{
    public IdleState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Idle, enterAct, exitAct, updateAct)
    {
    }
}