using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerUnitReturningResourceState : StateBase
{
    private WorkerUnit _workerUnit;
    private Vector3 _townHallLocation;
    public WorkerUnitReturningResourceState(WorkerUnit workerUnit)
    {
        _workerUnit = workerUnit;
    }

    public override void Tick()
    {
        base.Tick();

        if (Vector3.Distance(_townHallLocation, _workerUnit.transform.position) < 2f)
        {
            if(_workerUnit.CurrentTargetResource)
                _workerUnit.SendToGatherResource(_workerUnit.CurrentTargetResource);
            else
            {
                _workerUnit.SendToReGatherResource();

                // if (_workerUnit.CurrentResourceType == ResourceType.Gold){
                //     _workerUnit.GoToTravellingState(_townHallLocation + new Vector3(3,0,0));
                //     return;
                // }
                //
                // TreeManager closestTree = _workerUnit.FindNearestResourceOfType(
                //     _workerUnit.PreviousTargetResourceLocation,
                //     ResourceType.Lumber) as TreeManager;
                // if (closestTree)
                // {
                //     closestTree.SetReserved();
                //     _workerUnit.SendToGatherResource(closestTree);
                //     return;
                // }
                //
                // // if all else fails, just walk to the right hand side of the town hall
                // _workerUnit.GoToTravellingState(_townHallLocation + new Vector3(3,0,0));
            }
        }
    }

    public override void Enter()
    {
        base.Enter();

        BuildingBase closestTownHall =
            EntityManager.Instance.GetClosestBuilding(_workerUnit.transform.position, BuildingType.TownHall);

        _townHallLocation = _workerUnit.transform.position - new Vector3(3, 0, 0);

        
        if (closestTownHall != null)
            _townHallLocation = closestTownHall.transform.position;
        
        _workerUnit.Agent.SetDestination(_townHallLocation);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
