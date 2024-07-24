public class PlayerInteractionStateMachine
{
    private PlayerInteractionStateBase _currentState;
    
    public void Init()
    {
        
    }

    public void Tick()
    {
        if(_currentState == null)
            return;

        _currentState.Tick();
    }

    public void SetState(PlayerInteractionStateBase newState)
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

    private void EnterState(PlayerInteractionStateBase newState)
    {
        _currentState = newState;
        _currentState.Enter();
    }
}
