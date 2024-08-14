using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanIdleState : StateBase
{
    private FootmanManager _footmanManager;
    private float _enemyCheckTimer;
    
    public FootmanIdleState(FootmanManager footmanManager)
    {
        _footmanManager = footmanManager;
    }

    public override void Tick()
    {
        base.Tick();
        
        if (_enemyCheckTimer > 0)
        {
            _enemyCheckTimer -= Time.deltaTime;
        }
        else
        {
            UnitBase enemy = _footmanManager.CheckForEnemyInRange(_footmanManager.EnemyCheckRange);
        
            if (enemy)
            {
                //Debug.Log("Enemy: " + enemy.name + " Team: " + enemy.TeamID);
                _footmanManager.StateMachine.SetState(new FootmanMovingToAttackState(_footmanManager, enemy));
            }

            _enemyCheckTimer = _footmanManager.EnemyCheckFrequency;
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
