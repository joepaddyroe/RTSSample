using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductionPrefabs", menuName = "ScriptableObjects/ProductionPrefabsSO")]
public class ProductionPrefabsSO : ScriptableObject
{
    // need some cleverness here to decide what unit to return given a different faction perhaps?

    [SerializeField] private List<ProductionPackage> _productionPackages = new List<ProductionPackage>();
    public List<ProductionPackage> ProductionPackages => _productionPackages;
    
    public ProductionPackage GetProductionPackageByProductionType(ProductionType productionType)
    {
        return _productionPackages.FirstOrDefault(package  => package.ProductionType == productionType);
    }
    
}

[Serializable]
public class ProductionPackage
{
    public ProductionType ProductionType;
    public GameObject Prefab;
    public int GoldCost;
    public int LumberCost;
    public float ProductionTime;
}
