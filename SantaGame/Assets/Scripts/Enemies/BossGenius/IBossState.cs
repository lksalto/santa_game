using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState
{
    void EnterState(BossStateMachine boss);
    void UpdateState(BossStateMachine boss);
    void ExitState(BossStateMachine boss);
    void CheckStateTransition(BossStateMachine boss);
}
