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

            switch (_workerUnit.CurrentResourceType)
            {
                case ResourceType.Gold:
                    GameManager.Instance.AddGold(100);
                    break;
                case ResourceType.Lumber:
                    GameManager.Instance.AddLumber(100);
                    break;
            }
            
            if(_workerUnit.CurrentTargetResource)
                _workerUnit.SendToGatherResource(_workerUnit.CurrentTargetResource);
            else
            {
                _workerUnit.SendToReGatherResource();
            }
        }
    }

    public override void Enter()
    {
        base.Enter();

        BuildingBase closestTownHall =
            EntityManager.Instance.GetClosestBuilding(_workerUnit.transform.position, ConstructionType.TownHall, true);

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
