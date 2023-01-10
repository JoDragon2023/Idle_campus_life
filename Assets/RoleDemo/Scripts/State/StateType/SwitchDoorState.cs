using System;

/// <summary>
/// 开关柜门
/// </summary>
public class SwitchDoorState:StateTemplate
{
    public SwitchDoorState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.SwitchDoor, enterAct, exitAct, updateAct)
    {
    }
}