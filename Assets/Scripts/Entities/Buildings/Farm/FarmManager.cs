using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : BuildingBase
{
   public override void ConstructionComplete()
   {
      //need to do some checking to see add food only if this is on players team
      base.ConstructionComplete();
      GameManager.Instance.AddFood(3);
   }
   
   public override void ConstructionDestroyed()
   {
      //need to do some checking to see remove food only if this is on players team
      base.ConstructionDestroyed();
      GameManager.Instance.RemoveFood(3);
   }

   public override void DestroyOrDie()
   {
      base.DestroyOrDie();
      //ConstructionDestroyed();
      Destroy(gameObject, 2);
   }
}
