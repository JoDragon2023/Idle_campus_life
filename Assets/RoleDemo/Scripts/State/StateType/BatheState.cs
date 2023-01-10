using System;
//洗澡
public class BatheState:StateTemplate
{
    public BatheState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.Bathe, enterAct, exitAct, updateAct)
    {
    }
}

