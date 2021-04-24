using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {
    private Weapon weapon;
    private float countDown;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
    }

    public override void Enter() {
        base.Enter();
        if (countDown < 0.1f) {
            countDown = 1f / weapon.FireRate;
            weapon.ShootBullet();
        }
        isAbilityDone = true;
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }
    public override void LogicUpdate() {
        base.LogicUpdate();
        if (countDown > 0) countDown -= Time.deltaTime;
    }
}
