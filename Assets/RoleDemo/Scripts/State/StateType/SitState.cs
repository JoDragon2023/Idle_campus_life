using System;

/// <summary>
/// 坐下
/// </summary>
public class SitState:StateTemplate
{
    public SitState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Sit, enterAct, exitAct, updateAct)
    {
    }
}