using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeManager : ResourceBase, IWorkTargetEntity, IResourceGatherableTargetEntity
{
    [SerializeField] private int _lumberAmount;
    [SerializeField] private bool _reserved;
    
    public bool Reserved => _reserved;
    private bool _destroyed = false;
    public void GatherResource(IResourceGatheringAssignableEntity gatherer)
    {
        gatherer.GatherLumber(_lumberAmount);
        _lumberAmount -= _lumberAmount;

        if (_lumberAmount <= 0)
        {
            // need to do fancy tree falling animation here
            if (!_destroyed)
            {
                _destroyed = true;
                Destroy(gameObject);
            }
        }
    }

    public void SetReserved()
    {
        _reserved = true;
    }
    
    public void SetUnReserved()
    {
        _reserved = false;
    }
}
