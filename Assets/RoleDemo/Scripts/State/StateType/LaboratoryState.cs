using System;

/// <summary>
/// 科学实验室状态
/// </summary>
public class LaboratoryState:StateTemplate
{
    public LaboratoryState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Laboratory, enterAct, exitAct, updateAct)
    {
    }
}