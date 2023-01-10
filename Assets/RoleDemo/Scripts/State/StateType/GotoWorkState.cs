using System;
/// <summary>
/// 上班
/// </summary>
public class GotoWorkState:StateTemplate
{
    public GotoWorkState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.GotoWork, enterAct, exitAct, updateAct)
    {
    }
}