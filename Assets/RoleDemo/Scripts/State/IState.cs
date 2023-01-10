public interface IState 
{
    void OnEnter();   //进入状态的方法
    void OnUpdate(float deltaTime);  //维持状态的方法
    void OnExit();    //退出状态的方法
}
