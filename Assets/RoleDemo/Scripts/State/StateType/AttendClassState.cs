using System;
//上课
public class AttendClassState:StateTemplate
{
    public AttendClassState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.AttendClass, enterAct, exitAct, updateAct)
    {
    }
}