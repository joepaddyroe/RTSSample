using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : EntityBase, IMoveableEntity
{

    [SerializeField] private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;

    protected UISelectedUnit _uiSelectedUnit;

    public virtual void MoveToLocation(Vector3 location)
    {
        // should be handled on a case by case basis - for states etc
    }
    
    public virtual void GoToTravellingState(Vector3 targetDestination)
    {
        
    }
}
