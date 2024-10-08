using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitGatheringResourceState : StateBase
{
    private WorkerUnit _workerUnit;
    private IResourceGatherableTargetEntity _targetResource;
    private float _resourceGatheringInterval;
    private float _resourceGatheringTimer;
    private float _resourcePoint;
    
    public WorkerUnitGatheringResourceState(WorkerUnit workerUnit, IResourceGatherableTargetEntity targetResource, float resourceGatheringInterval, float resourcePoint)
    {
        _workerUnit = workerUnit;
        _targetResource = targetResource;
        _resourceGatheringInterval = resourceGatheringInterval;
        _resourcePoint = resourcePoint;
    }

    public override void Tick()
    {
        base.Tick();

        if (_resourceGatheringTimer < _resourceGatheringInterval)
            _resourceGatheringTimer += Time.deltaTime;
        else
        {
            _resourceGatheringTimer = 0;
            if (_targetResource != null)
            {
                _targetResource.GatherResource(_workerUnit as IResourceGatheringAssignableEntity);

                ResourceType resourceType = ResourceType.None;
                if (_targetResource as MineManager)
                    resourceType = ResourceType.Gold;
                if (_targetResource as TreeManager)
                    resourceType = ResourceType.Lumber;
                _workerUnit.GoToReturningResourceState(resourceType);
            }
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
        
        if(_targetResource as TreeManager)
            (_targetResource as TreeManager).SetUnReserved();
        
    }
}
