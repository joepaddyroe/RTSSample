using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerUnit : UnitBase, IWorkAssignableEntity, IResourceGatheringAssignableEntity
{
    // state
    private StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    private Vector3 _currentDestination;
    public Vector3 CurrentDestination => _currentDestination;

    // prefabs for construction
    [SerializeField] private ConstructionPrefabsSO _constructionPrefabsSO;
    public ConstructionPrefabsSO ConstructionPrefabsSO => _constructionPrefabsSO;
    
    [SerializeField] private float _constructionInterval;
    [SerializeField] private float _constructionPoint;

    [SerializeField] private float _resourceGatheringInterval;
    [SerializeField] private float _resourcePoint;

    private int _carryingGoldAmount;
    private int _carryingLumberAmount;
    private BuildingBase _currentTargetResource;
    private Vector3 _previousTargetResourceLocation;
    private ResourceType _currentResourceType;
    [SerializeField] private LayerMask _resourceLayerMask;

    public int CarryingGoldAmount => _carryingGoldAmount;
    public int CarryingLumberAmount => _carryingLumberAmount;
    public BuildingBase CurrentTargetResource => _currentTargetResource;
    public Vector3 PreviousTargetResourceLocation => _previousTargetResourceLocation;
    public ResourceType CurrentResourceType => _currentResourceType;
    public LayerMask ResourceLayerMask => _resourceLayerMask;
    
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    
    public override void Init()
    {
        base.Init();
        
        _stateMachine = new StateMachine();
        _stateMachine.SetState(new WorkerUnitIdleState(this));
        
        _uiSelectedUnit = _uiGame.AddUnitUI(_entitySelectionUI);
        _uiSelectedUnit.SetUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Tick();
        UpdateScreenSpaceCoordinate(this);
    }

    public void GoToIdlState()
    {
        _stateMachine.SetState(new WorkerUnitIdleState(this));
    }
    
    public override void GoToTravellingState(Vector3 targetDestination)
    {
        base.GoToTravellingState(targetDestination);
        _currentDestination = targetDestination;
        _stateMachine.SetState(new WorkerUnitMovingToDestinationState(this));
    }
    
    
    // building construction stuff
    public void GoToTargetConstruction(BuildingBase targetConstruction, Vector3 targetBuildSitePosition)
    {
        _stateMachine.SetState(new WorkerUnitMovingToConstructionState(this, targetConstruction, targetBuildSitePosition));
    }
    public void GoToConstructionState(BuildingBase targetConstruction)
    {
        _stateMachine.SetState(new WorkerUnitConstructingState(this, targetConstruction, _constructionInterval, _constructionPoint));
    }
    
    
    // resource gathering stuff
    public void GoToTargetResource(IResourceGatherableTargetEntity targetResource, Vector3 targetResourcePosition)
    {
        _stateMachine.SetState(new WorkerUnitMovingToResourceGatheringState(this, targetResource, targetResourcePosition));
    }
    public void GoToGatheringResourceState(IResourceGatherableTargetEntity targetResource)
    {
        _stateMachine.SetState(new WorkerUnitGatheringResourceState(this, targetResource, _resourceGatheringInterval, _resourcePoint));
    }

    public void GoToReturningResourceState(ResourceType currentResourceType)
    {
        _currentResourceType = currentResourceType;
        _stateMachine.SetState(new WorkerUnitReturningResourceState(this));
    }
    
    
    
    
    // Selection Handling
    public override void Select()
    {
        base.Select();
        _uiGame.SetWorkerUnitSelected(true, this, _constructionPrefabsSO.ConstructionPackages);
    }

    public override void DeSelect()
    {
        base.DeSelect();
        _uiGame.SetWorkerUnitSelected(false);
    }

    public void SendToConstructBuilding(BuildingBase building)
    {
        GoToTargetConstruction(building, building.transform.position);
    }

    public override void MoveToLocation(Vector3 location)
    {
        GoToTravellingState(location);
    }

    public void SendToGatherResource(BuildingBase building)
    {
        _currentTargetResource = building;
        _previousTargetResourceLocation = building.transform.position;
        GoToTargetResource(building as IResourceGatherableTargetEntity, building.transform.position);
    }

    public BuildingBase FindNearestResourceOfType(Vector3 previousTargetLocation, ResourceType resourceType)
    {
        BuildingBase closest = null;
        
        Collider[] resourceColliders = Physics.OverlapSphere(previousTargetLocation, 10, _resourceLayerMask);
        if (resourceColliders != null && resourceColliders.Length > 0)
        {
            // search colliders for closest tree to previous tree taken
            if (resourceType == ResourceType.Lumber)
            {
                TreeManager closestTree = null;
                float closestTreeDistance = Mathf.Infinity;
                    
                foreach (Collider resourceCollider in resourceColliders)
                {
                    if (resourceCollider.GetComponent<TreeManager>())
                    {
                        TreeManager tree = resourceCollider.GetComponent<TreeManager>();
                        if (!tree.Reserved)
                        {
                            float distance = Vector3.Distance(transform.position,
                                tree.transform.position);
                            if (distance < closestTreeDistance)
                            {
                                closestTreeDistance = distance;
                                closestTree = tree;
                            }
                        }
                    }
                }

                if (closestTree)
                {
                    return closestTree;
                }
            }
        }

        return null;
    }

    public void SendToResourceGathering(BuildingBase building)
    {
        
    }

    public void GatherGold(int amount)
    {
        _carryingGoldAmount = amount;
    }
    public void GatherLumber(int amount)
    {
        _carryingLumberAmount = amount;
    }
}

public enum ResourceType
{
    None,
    Gold,
    Lumber
}
