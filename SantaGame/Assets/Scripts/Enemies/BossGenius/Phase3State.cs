using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3State : IBossState
{
    public void EnterState(BossStateMachine boss)
    {
        //Debug.Log("Entrando fase 3");
        foreach (EnemyMono e in boss.shotguns)
        {

            e.StartShooting();
        }
        boss.GetComponentInChildren<ParticleSystem>().startColor = Color.yellow;
        boss.currentSprite = boss.sprites[2];
    }

    public void UpdateState(BossStateMachine boss)
    {
        
    }

    public void ExitState(BossStateMachine boss)
    {
        
    }
    public void CheckStateTransition(BossStateMachine boss)
    {
        if (boss.life > 0 && boss.life <= 26)
        {
            boss.TransitionToState(boss.phase4);
        }
    }
}
