using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : EnemyState {
    public bool isAnimationFinished;
    protected bool isTakingDamage;
    protected bool hitSideRight;
    protected float hitForceX, hitForceY;


    public DamagedState(Entity entity, EnemyStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName) {

    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
        isAnimationFinished = false;
        hitForceX = 4f;
        hitForceY = 5f;
        StartDamageAnimation();
    }


    public void HitSide(bool rightSide) {
        this.hitSideRight = rightSide;
    }

    public void SetHitForce(float x, float y) {
        this.hitForceX = x;
        this.hitForceY = y;
    }

    protected virtual void StartDamageAnimation() {
        // isAnimationFinished = false;
        if (!isTakingDamage) {
            isTakingDamage = true;
            if (hitSideRight) hitForceX *= -1;
            entity.RB.drag = 0f;
            // entity.RB.velocity = Vector2.zero;
            entity.RB.AddForce(new Vector2(hitForceX, hitForceY), ForceMode2D.Impulse);
            // isAnimationFinished = true;
            Debug.Log(isAnimationFinished);
            // sound
        }
    }


    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    // public virtual void TriggerDamaged() {
    //     StartDamageAnimation();
    // }
    public virtual void FinishDamaged() { isAnimationFinished = true; isTakingDamage = false; }

}
