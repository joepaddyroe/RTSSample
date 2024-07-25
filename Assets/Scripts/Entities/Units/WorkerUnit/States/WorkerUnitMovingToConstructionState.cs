using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitMovingToConstructionState : StateBase
{
    private WorkerUnit _workerUnit;
    private BuildingBase _targetConstruction;
    private Vector3 _targetBuildSitePosition;
    
    public WorkerUnitMovingToConstructionState(WorkerUnit workerUnit, BuildingBase targetConstruction, Vector3 targetBuildSitePosition)
    {
        _workerUnit = workerUnit;
        _targetConstruction = targetConstruction;
        _targetBuildSitePosition = targetBuildSitePosition;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_targetBuildSitePosition, _workerUnit.transform.position) < 2f)
        {
            _workerUnit.GoToConstructionState(_targetConstruction);
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _workerUnit.Agent.SetDestination(_targetBuildSitePosition);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
