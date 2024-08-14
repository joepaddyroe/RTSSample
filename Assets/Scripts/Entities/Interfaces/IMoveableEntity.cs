using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveableEntity
{
    void MoveToLocation(Vector3 location);
    void GoToTravellingState(Vector3 location);
}
