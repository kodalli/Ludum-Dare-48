using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data/Base Player Data")]
public class PlayerData : ScriptableObject {

    [Header("Move State")]
    public float movementVelocity = 5f;
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

}
