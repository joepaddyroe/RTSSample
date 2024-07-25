using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkAssignableEntity
{
    void SendToConstructBuilding(BuildingBase building);
    void SendToGatherResource(BuildingBase building);
}
