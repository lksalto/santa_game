using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2State : IBossState
{
    public void EnterState(BossStateMachine boss)
    {
        //Debug.Log("Entrando fase 2");
        boss.bomb.StartExplosion();
        boss.GetComponentInChildren<ParticleSystem>().startColor = Color.blue;
        boss.currentSprite = boss.sprites[1];
    }

    public void UpdateState(BossStateMachine boss)
    {
        
    }

    public void ExitState(BossStateMachine boss)
    {
        
    }

    public void CheckStateTransition(BossStateMachine boss)
    {
        if (boss.life > 26 && boss.life <= 52)
        {
            boss.TransitionToState(boss.phase3);
        }
    }
}
