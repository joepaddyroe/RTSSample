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
    private ResourceBase _currentTargetResource;
    private Vector3 _previousTargetResourceLocation;
    private ResourceType _currentResourceType;
    [SerializeField] private LayerMask _resourceLayerMask;

    public int CarryingGoldAmount => _carryingGoldAmount;
    public int CarryingLumberAmount => _carryingLumberAmount;
    public ResourceBase CurrentTargetResource => _currentTargetResource;
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
    public void GoToTargetResource(ResourceType resourceType, Vector3 targetResourcePosition)
    {
        _stateMachine.SetState(new WorkerUnitMovingToResourceGatheringState(this, resourceType, targetResourcePosition));
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

    public void SendToGatherResource(ResourceBase resource)
    {
        _currentTargetResource = resource;
        _currentResourceType = resource.ResourceType;
        _previousTargetResourceLocation = resource.transform.position;
        GoToTargetResource(resource.ResourceType, resource.transform.position);
    }
    
    public void SendToReGatherResource()
    {
        GoToTargetResource(_currentResourceType, _previousTargetResourceLocation);
    }

    public ResourceBase FindNearestResourceOfType(Vector3 location, ResourceType resourceType)
    {
        Collider[] resourceColliders = Physics.OverlapSphere(location, 10, _resourceLayerMask);
        if (resourceColliders != null && resourceColliders.Length > 0)
        {
            // search colliders for closest tree
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
                            float distance = Vector3.Distance(location,
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
                    closestTree.SetReserved();
                    return closestTree;
                }
            }
            
            // search colliders for closest mine
            if (resourceType == ResourceType.Gold)
            {
                MineManager closestMine = null;
                float closestMineDistance = Mathf.Infinity;
                    
                foreach (Collider resourceCollider in resourceColliders)
                {
                    //Debug.Log("Found resource: " + resourceCollider.name);
                    
                    if (resourceCollider.GetComponent<MineManager>())
                    {
                        MineManager mine = resourceCollider.GetComponent<MineManager>();
                        float distance = Vector3.Distance(location, mine.transform.position);
                        if (distance < closestMineDistance)
                        {
                            closestMineDistance = distance;
                            closestMine = mine;
                        }
                    }
                }

                if (closestMine)
                {
                    return closestMine;
                }
            }
        }

        return null;
    }

    public void SendToResourceGathering(ResourceBase resource)
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
