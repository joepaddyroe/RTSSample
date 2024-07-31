using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBase : EntityBase, IWorkTargetEntity
{
    protected UISelectedBuilding _uiSelectedBuilding;
    
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] protected Animator _animator;

    public ResourceType ResourceType => _resourceType;
    
    public void Start()
    {
        
    }
    
    public GameObject GetWorkTarget()
    {
        return gameObject;
    }
}
