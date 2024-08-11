using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanMovingToAttackState : StateBase
{
    private FootmanManager _footmanManager;
    private UnitBase _attackTarget;
    
    public FootmanMovingToAttackState(FootmanManager footmanManager, UnitBase attackTarget)
    {
        _footmanManager = footmanManager;
        _attackTarget = attackTarget;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_attackTarget.transform.position, _footmanManager.transform.position) < 2f)
        {
            _footmanManager.StateMachine.SetState(new FootmanAttackState(_footmanManager, _attackTarget));
        }
        else
        {
            _footmanManager.Agent.SetDestination(_attackTarget.transform.position);
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _footmanManager.Agent.SetDestination(_attackTarget.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
