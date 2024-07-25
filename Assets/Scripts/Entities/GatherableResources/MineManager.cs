using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : BuildingBase, IWorkTargetEntity, IResourceGatherableTargetEntity
{
    [SerializeField] private int _goldAmount;
    
    public void GatherResource(IResourceGatheringAssignableEntity gatherer)
    {
        if (_goldAmount >= 100)
        {
            _goldAmount -= 100;
            gatherer.GatherGold(100);
        }
        else
        {
            gatherer.GatherGold(_goldAmount);
            _goldAmount = 0;
        }

        if (_goldAmount == 0)
        {
            // need o do fancy gold mine destruction animation here
            Destroy(gameObject);   
        }
    }
}
