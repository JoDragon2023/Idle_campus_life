using System;

/// <summary>
/// 丢垃圾
/// </summary>
public class LoseGarbageState:StateTemplate
{
    public LoseGarbageState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.LoseGarbage, enterAct, exitAct, updateAct)
    {
    }    
}