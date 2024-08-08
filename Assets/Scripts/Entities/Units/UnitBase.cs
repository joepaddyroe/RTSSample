using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : EntityBase, IMoveableEntity
{

    [SerializeField] private ProductionType _unitType;
    [SerializeField] private NavMeshAgent _agent;

    public ProductionType UnitType => _unitType;
    public NavMeshAgent Agent => _agent;

    protected UISelectedUnit _uiSelectedUnit;

    public virtual void MoveToLocation(Vector3 location)
    {
        // should be handled on a case by case basis - for states etc
    }
    
    public virtual void GoToTravellingState(Vector3 targetDestination)
    {
        
    }

    public virtual void UpdateScreenSpaceCoordinate(UnitBase unitBase)
    {
        _uiGame.UpdateUnitScreenSpaceCoordinate(unitBase, transform.position);
    }
}
