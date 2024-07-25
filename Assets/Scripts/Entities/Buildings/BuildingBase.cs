using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : EntityBase, IWorkTargetEntity
{
    protected UISelectedBuilding _uiSelectedBuilding;
    
    // construction values
    protected bool _constructed;
    protected float _constructionProgress;
    
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] protected bool _preConstructed;
    [SerializeField] protected float _constructionProgressTarget;
    [SerializeField] protected Animator _animator;

    public BuildingType BuildingType => _buildingType;
    public bool Constructed => _constructed;
    
    public void Start()
    {
        if (_preConstructed)
        {
            Construct(_constructionProgressTarget);
            _constructed = true;
            EntityManager.Instance.AddBuilding(this);
        }
    }

    public virtual bool Construct(float constructionPoint)
    {
        if (_constructionProgress < _constructionProgressTarget)
        {
            _constructionProgress = Mathf.Clamp(_constructionProgress + constructionPoint, 0, _constructionProgressTarget);
            _animator.SetFloat("Construction", _constructionProgress/_constructionProgressTarget);
            return false;
        }
        else
        {
            _constructed = true;
            _animator.SetFloat("Construction", _constructionProgress/_constructionProgressTarget);
            return true;
        }
    }

    public GameObject GetWorkTarget()
    {
        return gameObject;
    }
}
