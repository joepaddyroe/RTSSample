using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : EntityBase, IMoveableEntity
{

    [SerializeField] private ProductionType _unitType;
    [SerializeField] private bool _alive;

    
    [SerializeField] private NavMeshAgent _agent;
    
    [SerializeField] private float _enemyCheckFrequency;
    [SerializeField] private float _enemyCheckRange;
    [SerializeField] private LayerMask _enemyCheckLayerMask;

    [SerializeField] private GameObject _characterSprite;
    
    public ProductionType UnitType => _unitType;
    public NavMeshAgent Agent => _agent;
    
    public float EnemyCheckFrequency => _enemyCheckFrequency;
    public float EnemyCheckRange => _enemyCheckRange;

    public GameObject CharacterSprite => _characterSprite;

    public bool Alive => _alive;
    
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

    public void SetTeamID(int teamID)
    {
        _teamID = teamID;
    }

    public UnitBase CheckForEnemyInRange(float range)
    {
        Collider[] unitColliders = Physics.OverlapSphere(transform.position, range, _enemyCheckLayerMask);
        
        UnitBase closestUnit = null;
        float closestUnitDistance = Mathf.Infinity;
                    
        foreach (Collider unitCollider in unitColliders)
        {
            UnitBase testUnit = unitCollider.GetComponent<UnitBase>();
            if (testUnit)
            {
                if (testUnit.TeamID != _teamID && testUnit.Alive)
                {
                    float distance = Vector3.Distance(transform.position,
                        testUnit.transform.position);
                    if (distance < closestUnitDistance)
                    {
                        closestUnitDistance = distance;
                        closestUnit = testUnit;
                    }
                }
            }
        }
        return closestUnit;
    }

    public void TakeDamage(float damage)
    {
        if (!Alive)
            return;
        
        if (_health > damage)
        {
            _health -= damage;
        }
        else
        {
            _health = 0;
            _alive = false;
            Die();
        }
        
        Debug.Log("Hit! " + _health);
    }

    public virtual void Die()
    {
        
    }
}
