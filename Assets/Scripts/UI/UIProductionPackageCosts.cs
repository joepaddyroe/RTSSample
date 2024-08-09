using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIProductionPackageCosts : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldValue;
    [SerializeField] private TMP_Text _lumberValue;
    [SerializeField] private TMP_Text _foodValue;

    public void SetValues(int gold, int lumber, int food)
    {
        _goldValue.text = gold.ToString();
        _lumberValue.text = lumber.ToString();
        _foodValue.text = food.ToString();
    }

    private void Update()
    {
        transform.position = Input.mousePosition - new Vector3(0, 120, 0);
    }
}
