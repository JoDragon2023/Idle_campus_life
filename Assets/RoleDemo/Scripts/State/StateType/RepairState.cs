using System;

/// <summary>
/// 维修
/// </summary>
public class RepairState:StateTemplate
{
    public RepairState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Repair, enterAct, exitAct, updateAct)
    {
    }
}