using System;
//吃喝
public class EatDrinkState:StateTemplate
{
    public EatDrinkState(Action enterAct, Action exitAct, Action<float> updateAct)
        : base(RoleState.EatDrink, enterAct, exitAct, updateAct)
    {
    }
}