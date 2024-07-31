using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private static EntityManager instance;

    public static EntityManager Instance
    {
        get
        {
            return instance != null
                ? instance
                : instance = new GameObject("EntityManager").AddComponent<EntityManager>();
        }
    }

    private Dictionary<UnitType, List<UnitBase>> _units = new Dictionary<UnitType, List<UnitBase>>();
    private Dictionary<BuildingType, List<BuildingBase>> _buildings = new Dictionary<BuildingType, List<BuildingBase>>();

    public Dictionary<UnitType, List<UnitBase>> Units => _units;
    public Dictionary<BuildingType, List<BuildingBase>> Buildings => _buildings;

    // units
    public void AddUnit(UnitBase unit)
    {
        if(_units.ContainsKey(unit.UnitType))
            _units[unit.UnitType].Add(unit);
        else
            _units.Add(unit.UnitType, new List<UnitBase>(){unit});
    }
    
    public void RemoveUnit(UnitBase unit)
    {
        if(_units.ContainsKey(unit.UnitType))
            if(_units[unit.UnitType].Contains(unit))
                _units[unit.UnitType].Remove(unit);
    }
    public UnitBase GetClosestUnit(Vector3 position, UnitType unitType)
    {
        UnitBase returnUnit = null;
        float closest = Mathf.Infinity;
        foreach (UnitBase unit in _units[unitType])
        {
            float distance = Vector3.Distance(position, unit.transform.position);
            if (distance < closest)
            {
                closest = distance;
                returnUnit = unit;
            }   
        }
        return returnUnit;
    }
    
    
    
    // buildings
    public void AddBuilding(BuildingBase building)
    {
        if(_buildings.ContainsKey(building.BuildingType))
            _buildings[building.BuildingType].Add(building);
        else
            _buildings.Add(building.BuildingType, new List<BuildingBase>(){building});
    }

    public void RemoveBuilding(BuildingBase building)
    {
        if(_buildings.ContainsKey(building.BuildingType))
            if(_buildings[building.BuildingType].Contains(building))
                _buildings[building.BuildingType].Remove(building);
    }
    public BuildingBase GetClosestBuilding(Vector3 position, BuildingType buildingType)
    {
        BuildingBase returnBuilding = null;
        float closest = Mathf.Infinity;
        foreach (BuildingBase building in _buildings[buildingType])
        {
            float distance = Vector3.Distance(position, building.transform.position);
            if (distance < closest)
            {
                closest = distance;
                returnBuilding = building;
            }   
        }
        return returnBuilding;
    }
    
}

public enum UnitType
{
    Peasant,
    Footman
}

public enum BuildingType
{
    Tree,
    Mine,
    TownHall,
    Farm,
    Barracks
}

public enum ResourceType
{
    None,
    Gold,
    Lumber
}
