using System;
//去上课
public class GoToClassState:StateTemplate
{
    public GoToClassState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.GoToClass, enterAct, exitAct, updateAct)
    {
        
    }
}