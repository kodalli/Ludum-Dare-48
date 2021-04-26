using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour {
    public AttackState attackState;
    public DamagedState damagedState;

    private void Start() {
        damagedState = GetComponentInParent<Enemy1>().damagedState;
    }
    private void TriggerAttack() => attackState.TriggerAttack();
    private void FinishAttack() => attackState.FinishAttack();

    // private void TriggerDamaged() => GetComponentInParent<Enemy1>().damagedState.TriggerDamaged();
    public void FinishDamagedAnimation() {
        var x = GetComponentInParent<Enemy1>().damagedState;
        Debug.Log("hi");
        Debug.Log(x);
        x.FinishDamaged();
    }

}


