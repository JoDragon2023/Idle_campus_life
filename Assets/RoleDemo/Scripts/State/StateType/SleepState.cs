using System;

/// <summary>
/// 睡觉
/// </summary>
public class SleepState:StateTemplate
{
    public SleepState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Sleep, enterAct, exitAct, updateAct)
    {
    }
}