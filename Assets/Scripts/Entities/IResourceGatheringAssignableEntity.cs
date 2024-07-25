using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceGatheringAssignableEntity
{
    void SendToResourceGathering(BuildingBase building);
    void GatherGold(int amount);
}
