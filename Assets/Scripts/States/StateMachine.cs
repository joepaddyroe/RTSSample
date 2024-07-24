public class StateMachine
{
    private StateBase _currentState;
    
    public void Init()
    {
        
    }

    public void Tick()
    {
        if(_currentState == null)
            return;

        _currentState.Tick();
    }

    public void SetState(StateBase newState)
    {
        ExitState();
        EnterState(newState);
    }

    private void ExitState()
    {
        if(_currentState == null)
            return;
        
        _currentState.Exit();
    }

    private void EnterState(StateBase newState)
    {
        _currentState = newState;
        _currentState.Enter();
    }
}
