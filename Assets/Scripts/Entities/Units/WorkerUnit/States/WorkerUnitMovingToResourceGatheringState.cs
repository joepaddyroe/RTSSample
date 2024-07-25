using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitMovingToResourceGatheringState : StateBase
{
    private WorkerUnit _workerUnit;
    private IResourceGatherableTargetEntity _targetResource;
    private Vector3 _targetResourcePosition;
    
    public WorkerUnitMovingToResourceGatheringState(WorkerUnit workerUnit, IResourceGatherableTargetEntity targetResource, Vector3 targetResourcePosition)
    {
        _workerUnit = workerUnit;
        _targetResource = targetResource;
        _targetResourcePosition = targetResourcePosition;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_targetResourcePosition, _workerUnit.transform.position) < 2f)
        {
            _workerUnit.GoToGatheringResourceState(_targetResource);
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        _workerUnit.Agent.SetDestination(_targetResourcePosition);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
