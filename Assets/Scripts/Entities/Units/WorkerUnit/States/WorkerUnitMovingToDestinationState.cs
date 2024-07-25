using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitMovingToDestinationState : StateBase
{
    private WorkerUnit _workerUnit;
    
    public WorkerUnitMovingToDestinationState(WorkerUnit workerUnit)
    {
        _workerUnit = workerUnit;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_workerUnit.CurrentDestination, _workerUnit.transform.position) < 0.1f)
        {
            _workerUnit.StateMachine.SetState(new WorkerUnitIdleState(_workerUnit));
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _workerUnit.Agent.SetDestination(_workerUnit.CurrentDestination);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
