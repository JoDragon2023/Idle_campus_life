using System;

/// <summary>
/// 头部动作
/// </summary>
public class HeadActState:StateTemplate
{
    public HeadActState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.HeadAct, enterAct, exitAct, updateAct)
    {
    }
}