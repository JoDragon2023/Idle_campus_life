using System;

/// <summary>
/// 交谈
/// </summary>
public class TalkState:StateTemplate
{
    public TalkState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Talk, enterAct, exitAct, updateAct)
    {
    }    
}