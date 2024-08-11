using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanMovingToDestinationState : StateBase
{
    private FootmanManager _footmanManager;
    
    public FootmanMovingToDestinationState(FootmanManager footmanManager)
    {
        _footmanManager = footmanManager;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_footmanManager.CurrentDestination, _footmanManager.transform.position) < 1.5f)
        {
            _footmanManager.StateMachine.SetState(new FootmanIdleState(_footmanManager));
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _footmanManager.Agent.SetDestination(_footmanManager.CurrentDestination);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
