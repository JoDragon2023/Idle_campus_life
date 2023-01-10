using System;

//打扫垃圾
public class CleanGarbageState:StateTemplate
{
    public CleanGarbageState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.CleanGarbage , enterAct, exitAct, updateAct)
    {
    }
}