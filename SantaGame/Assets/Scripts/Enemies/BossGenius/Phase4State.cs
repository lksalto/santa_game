using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase4State : IBossState
{
    public void EnterState(BossStateMachine boss)
    {
        //Debug.Log("Entrando fase 4");
        //boss.follow.canMove = true;
        boss.spin.GetComponent<EnemyMono>().StartShooting();

        boss.GetComponentInChildren<ParticleSystem>().startColor = Color.red;
        boss.currentSprite = boss.sprites[3];
    }

    public void UpdateState(BossStateMachine boss)
    {
        
    }

    public void ExitState(BossStateMachine boss)
    {
        
    }

    public void CheckStateTransition(BossStateMachine boss)
    {
        if (boss.life <= 0)
        {
            boss.Die();
        }
    }
}
