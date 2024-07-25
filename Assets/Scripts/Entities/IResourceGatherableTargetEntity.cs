using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceGatherableTargetEntity
{
    void GatherResource(IResourceGatheringAssignableEntity gatherer);
}
