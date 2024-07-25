using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitReturningResourceState : StateBase
{
    private WorkerUnit _workerUnit;
    
    public WorkerUnitReturningResourceState(WorkerUnit workerUnit)
    {
        _workerUnit = workerUnit;
    }

    public override void Tick()
    {
        base.Tick();

        
    }

    public override void Enter()
    {
        base.Enter();
        
        _workerUnit.Agent.SetDestination(_workerUnit.transform.position - new Vector3(3,0,0));
    }

    public override void Exit()
    {
        base.Exit();
    }
}
