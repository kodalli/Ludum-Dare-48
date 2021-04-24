using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


public enum CombatInputs {
    PRIMARY,
}
public class PlayerInputHandler : MonoBehaviour {

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool[] AttackInputs { get; private set; }

    private void Start() {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    public void OnMoveInput(InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context) {
        if (context.started) { AttackInputs[(int)CombatInputs.PRIMARY] = true; }
        if (context.canceled) { AttackInputs[(int)CombatInputs.PRIMARY] = false; }
    }

}
