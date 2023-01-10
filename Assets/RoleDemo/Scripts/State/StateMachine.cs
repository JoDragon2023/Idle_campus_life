using System.Collections.Generic;

public class StateMachine 
{
    public Dictionary<RoleState, StateTemplate> m_stateCache;
    private StateTemplate m_previousState;
    private StateTemplate m_currentState;
    public float currentTime;

    private bool isChanging;

    private Queue<RoleState> stateQueue;

    private bool canBreak;
    
    public bool CanBreak
    {
        get => canBreak;
        set
        {
            canBreak = value;
        }
    }
    
    public StateMachine(StateTemplate beginState)
    {
        m_currentState = beginState;

        m_stateCache = new Dictionary<RoleState, StateTemplate>();

        stateQueue = new Queue<RoleState>();

        InitState(beginState);

        GameManager.Instance.UpdateAct += Update;
    }
    
    public void InitState(StateTemplate state)
    {
        if (m_stateCache.ContainsKey(state.state))
            return;

        m_stateCache.Add(state.state, state);
        state.machine = this;
    }
    
    
    public void TranslateState(RoleState state)
    {
        if (!m_stateCache.ContainsKey(state))
            return;

        if (isChanging)
        {
            return;
        }

        isChanging = true;
        m_previousState = m_currentState;
        m_currentState = m_stateCache[state];
        m_previousState.OnExit();
        //重置打断状态
        CanBreak = true;
        m_currentState.OnEnter();
        currentTime = 0;
        isChanging = false;
    }
    
    public void Update(float deltaTime)
    {
        if (m_currentState != null)
        {
            m_currentState.OnUpdate(deltaTime);
        }
        currentTime += deltaTime;
    }
    
    
    public void TranslateNextState()
    {
        RoleState state;
        if (stateQueue.Count < 1)
            state = RoleState.Run;
        else
            state = stateQueue.Dequeue();
        TranslateState(state);
    }

    public void AddState(RoleState state)
    {
        stateQueue.Enqueue(state);
    }

    public StateTemplate GetCurrentState()
    {
        return m_currentState;
    }

    public bool HasNextState()
    {
        return stateQueue.Count > 0;
    }

    public void TryTranslateState(RoleState state)
    {
        if (CanBreak)
            TranslateState(state);
    }

    public void Release()
    {
        foreach (var item in m_stateCache)
        {
            item.Value.Release();
        }
        m_stateCache.Clear();
        m_stateCache = null;
    }
    
    
}
