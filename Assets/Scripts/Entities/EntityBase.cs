using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour, ISelectableEntity
{
    
    // shared
    [SerializeField] protected GameObject _entitySelectionUI;
    [SerializeField] protected string _name;
    [SerializeField] protected float _health;
    [SerializeField] protected int _teamID;
    
    protected bool _selected;
    
    protected UIGame _uiGame;

    public bool IsSelected => _selected;
    public int TeamID => _teamID;
    
    public virtual void Init()
    {
        _uiGame = GameObject.Find("UIGame").GetComponent<UIGame>();
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;
    }


    public virtual void Select()
    {
        _selected = true;
    }

    public virtual void DeSelect()
    {
        _selected = false;
    }
    
}
