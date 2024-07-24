using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanIdleState : StateBase
{
    private FootmanManager _footmanManager;
    
    public FootmanIdleState(FootmanManager footmanManager)
    {
        _footmanManager = footmanManager;
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Enter()
    {
        base.Enter();
        _footmanManager.Agent.SetDestination(_footmanManager.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
