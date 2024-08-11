using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitDeadState : StateBase
{
    private WorkerUnit _workerUnit;
    
    public WorkerUnitDeadState(WorkerUnit peasantManager)
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
        _workerUnit.Agent.enabled = false;
        _workerUnit.CharacterSprite.transform.rotation = Quaternion.Euler(90,0,0);
        _workerUnit.CharacterSprite.transform.localPosition = new Vector3(0,-1,0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
