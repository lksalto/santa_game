using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1State : IBossState
{
   public void EnterState(BossStateMachine boss)
    {
        //Debug.Log("Entrando fase 1");
        boss.geniusAim.gameObject.SetActive(true);
        boss.GetComponentInChildren<ParticleSystem>().startColor = Color.green;
    }

    public void UpdateState(BossStateMachine boss)
    {
        Debug.Log("Update fase 1");
    }

    public void ExitState(BossStateMachine boss)
    {
        Debug.Log("Saindo fase 1");
    }
    public void CheckStateTransition(BossStateMachine boss)
    {

        if (boss.life > 52 && boss.life <= 78)
        {
            boss.TransitionToState(boss.phase2);
        }
    }
}
