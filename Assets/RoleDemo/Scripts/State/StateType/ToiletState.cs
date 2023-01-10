using System;

/// <summary>
/// 上厕所
/// </summary>
public class ToiletState:StateTemplate
{
    public ToiletState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Toilet, enterAct, exitAct, updateAct)
    {
    }
}