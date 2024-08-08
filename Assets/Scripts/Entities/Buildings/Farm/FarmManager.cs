using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : BuildingBase
{
   public override void ConstructionComplete()
   {
      base.ConstructionComplete();
      GameManager.Instance.AddFood(3);
   }
   
   public override void ConstructionDestroyed()
   {
      base.ConstructionDestroyed();
      GameManager.Instance.RemoveFood(3);
   }
}
