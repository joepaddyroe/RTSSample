using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitMovingToResourceGatheringState : StateBase
{
    private WorkerUnit _workerUnit;
    private IResourceGatherableTargetEntity _targetResource;
    private Vector3 _targetResourcePosition;
    private ResourceType _targetResourceType;
    
    public WorkerUnitMovingToResourceGatheringState(WorkerUnit workerUnit, ResourceType targetResourceType, Vector3 targetResourcePosition)
    {
        _workerUnit = workerUnit;
        _targetResourceType = targetResourceType;
        _targetResourcePosition = targetResourcePosition;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_targetResourcePosition, _workerUnit.transform.position) < 2f)
        {
            if (_targetResource == null)
            {
                _targetResource = FindClosestResourceOfType(_targetResourceType);
            
                if (_targetResource != null)
                {
                    if (Vector3.Distance((_targetResource as ResourceBase).transform.position,
                            _workerUnit.transform.position) >= 2)
                    {
                        _targetResourcePosition = (_targetResource as ResourceBase).transform.position;
                        _workerUnit.Agent.SetDestination(_targetResourcePosition);
                    }
                }
                else
                {
                    _workerUnit.GoToIdlState();
                }
            }
            else
            {
                _workerUnit.GoToGatheringResourceState(_targetResource);
            }
        }
    }

    private IResourceGatherableTargetEntity FindClosestResourceOfType(ResourceType resourceType)
    {
        return _workerUnit.FindNearestResourceOfType(_workerUnit.transform.position, resourceType) as IResourceGatherableTargetEntity;
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
