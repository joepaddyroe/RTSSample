using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitIdleState : StateBase
{
    private WorkerUnit _workerUnit;
    
    public WorkerUnitIdleState(WorkerUnit peasantManager)
    {
        _workerUnit = peasantManager;
    }

    public override void Tick()
    {
        base.Tick();
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
