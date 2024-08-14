using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanAttackState : StateBase
{
    private FootmanManager _footmanManager;
    private EntityBase _attackTarget;
    
    private float _attackTimer = 0;
    
    public FootmanAttackState(FootmanManager footmanManager, EntityBase attackTarget)
    {
        _footmanManager = footmanManager;
        _attackTarget = attackTarget;
    }

    public override void Tick()
    {
        base.Tick();

        if (_attackTarget == null)
            _footmanManager.StateMachine.SetState(new FootmanIdleState(_footmanManager));
        
        if(!_attackTarget.Functioning)
            _footmanManager.StateMachine.SetState(new FootmanIdleState(_footmanManager));
        
        if (Vector3.Distance(_attackTarget.transform.position, _footmanManager.transform.position) > 2f)
            _footmanManager.StateMachine.SetState(new FootmanMovingToAttackState(_footmanManager, _attackTarget));
        
        
        if (_attackTimer > 0)
        {
            _attackTimer -= Time.deltaTime;
        }
        else
        {
            // do attack animation stuff
            _attackTimer = _footmanManager.AttackInterval;
            
            _attackTarget.TakeDamage(_footmanManager.AttackPoint);
        }
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
