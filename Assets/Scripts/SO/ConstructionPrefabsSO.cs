using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionPrefabs", menuName = "ScriptableObjects/ConstructionPrefabsSO")]
public class ConstructionPrefabsSO : ScriptableObject
{
    // need some cleverness here to decide what building to return given a different faction perhaps?

    [SerializeField] private List<ConstructionPackage> _constructionPackages = new List<ConstructionPackage>();
    public List<ConstructionPackage> ConstructionPackages => _constructionPackages;
    
    public ConstructionPackage GetConstructionPackageByConstructionType(ConstructionType constructionType)
    {
        return _constructionPackages.FirstOrDefault(package  => package.ConstructionType == constructionType);
    }
    
}

[Serializable]
public class ConstructionPackage
{
    public ConstructionType ConstructionType;
    public GameObject Prefab;
    public int GoldCost;
    public int LumberCost;
    public float ConstructionTime;
}
