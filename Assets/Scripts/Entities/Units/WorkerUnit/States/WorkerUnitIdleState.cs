using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitIdleState : StateBase
{
    private WorkerUnit _workerUnit;
    private float _enemyCheckTimer;
    
    public WorkerUnitIdleState(WorkerUnit peasantManager)
    {
        _workerUnit = peasantManager;
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
            UnitBase enemy = _workerUnit.CheckForEnemyInRange(_workerUnit.EnemyCheckRange);
        
            if (enemy)
            {
                //Debug.Log("Enemy: " + enemy.name + " Team: " + enemy.TeamID);
            }

            _enemyCheckTimer = _workerUnit.EnemyCheckFrequency;
        }
    }

    public override void Enter()
    {
        base.Enter();
        _workerUnit.Agent.SetDestination(_workerUnit.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
