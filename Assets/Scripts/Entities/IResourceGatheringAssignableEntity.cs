using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceGatheringAssignableEntity
{
    void SendToResourceGathering(ResourceBase resource);
    void GatherGold(int amount);
    void GatherLumber(int amount);
}
