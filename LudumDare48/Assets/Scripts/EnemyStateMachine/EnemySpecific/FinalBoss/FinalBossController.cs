using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour, IDamageable {

    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Vector2 hitForce;
    [SerializeField] private float maxHealth;
    [SerializeField] private float contactDamage;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private float drag;
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private float currentHealth;
    private bool isPlayerInRange;
    private bool isDetected;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float damage) {
        throw new System.NotImplementedException();
    }


    public void TakeDamage(float damage, bool hitFromRight) {
        throw new System.NotImplementedException();
    }


    private void OnCollisionEnter2D(Collision2D other) {

    }

    private void OnTriggerEnter2D(Collider2D other) {

    }

    bool CheckPlayerInRange() => Physics2D.Raycast(playerCheck.position, -Vector2.right, playerCheckDistance, whatIsPlayer);
}
