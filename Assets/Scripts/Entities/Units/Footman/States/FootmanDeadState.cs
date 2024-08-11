using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanDeadState : StateBase
{
    private FootmanManager _footmanManager;
    
    public FootmanDeadState(FootmanManager footmanManager)
    {
        _footmanManager = footmanManager;
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Enter()
    {
        base.Enter();
        _footmanManager.Agent.SetDestination(_footmanManager.transform.position);
        _footmanManager.Agent.enabled = false;
        _footmanManager.CharacterSprite.transform.rotation = Quaternion.Euler(90,0,0);
        _footmanManager.CharacterSprite.transform.localPosition = new Vector3(0,-1,0);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
